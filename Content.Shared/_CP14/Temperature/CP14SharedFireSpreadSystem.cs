using Content.Shared.DoAfter;
using Content.Shared.IdentityManagement;
using Content.Shared.Interaction;
using Content.Shared.Popups;
using Content.Shared.Weapons.Melee.Events;
using Robust.Shared.Serialization;
using Robust.Shared.Timing;

namespace Content.Shared._CE14.Temperature;

public abstract partial class CE14SharedFireSpreadSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _gameTiming = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14FireSpreadComponent, OnFireChangedEvent>(OnFireChangedSpread);
        SubscribeLocalEvent<CE14DespawnOnExtinguishComponent, OnFireChangedEvent>(OnFireChangedDespawn);
        SubscribeLocalEvent<CE14DelayedIgnitionSourceComponent, OnFireChangedEvent>(OnIgnitionSourceFireChanged);
        SubscribeLocalEvent<CE14DelayedIgnitionSourceComponent, AfterInteractEvent>(OnDelayedIgniteAttempt);
    }

    private void OnFireChangedDespawn(Entity<CE14DespawnOnExtinguishComponent> ent, ref OnFireChangedEvent args)
    {
        if (!args.OnFire)
            QueueDel(ent);
    }

    private void OnFireChangedSpread(Entity<CE14FireSpreadComponent> ent, ref OnFireChangedEvent args)
    {
        if (args.OnFire)
        {
            EnsureComp<CE14ActiveFireSpreadingComponent>(ent);
        }
        else
        {
            if (HasComp<CE14ActiveFireSpreadingComponent>(ent))
                RemCompDeferred<CE14ActiveFireSpreadingComponent>(ent);
        }

        ent.Comp.NextSpreadTime = _gameTiming.CurTime + TimeSpan.FromSeconds(ent.Comp.SpreadCooldownMax);
    }

    private void OnDelayedIgniteAttempt(Entity<CE14DelayedIgnitionSourceComponent> ent, ref AfterInteractEvent args)
    {
        if (!args.CanReach || args.Handled || args.Target == null)
            return;

        if (!ent.Comp.Enabled)
            return;

        var time = ent.Comp.Delay;
        var caution = true;

        if (TryComp<CE14IgnitionModifierComponent>(args.Target, out var modifier))
        {
            time *= modifier.IgnitionTimeModifier;
            caution = !modifier.HideCaution;
        }

        _doAfter.TryStartDoAfter(new DoAfterArgs(EntityManager,
            args.User,
            time,
            new CE14IgnitionDoAfter(),
            args.Target,
            args.Target,
            ent)
        {
            BreakOnDamage = true,
            BreakOnMove = true,
            BreakOnDropItem = true,
            BreakOnHandChange = true,
            BlockDuplicate = true,
            CancelDuplicate = true
        });

        var selfMessage = Loc.GetString("CE14-attempt-ignite-caution-self",
            ("target", MetaData(args.Target.Value).EntityName));
        var otherMessage = Loc.GetString("CE14-attempt-ignite-caution",
            ("name", Identity.Entity(args.User, EntityManager)),
            ("target", Identity.Entity(args.Target.Value, EntityManager)));
        _popup.PopupPredicted(selfMessage,
            otherMessage,
            args.User,
            args.User,
            caution ? PopupType.MediumCaution : PopupType.Small);
    }

    private void OnIgnitionSourceFireChanged(Entity<CE14DelayedIgnitionSourceComponent> ent, ref OnFireChangedEvent args)
    {
        ent.Comp.Enabled = args.OnFire;
        Dirty(ent);
    }
}

/// <summary>
/// Raised whenever an FlammableComponent OnFire is Changed
/// </summary>
[ByRefEvent]
public readonly record struct OnFireChangedEvent(bool OnFire)
{
    public readonly bool OnFire = OnFire;
}

[Serializable, NetSerializable]
public sealed partial class CE14IgnitionDoAfter : SimpleDoAfterEvent
{
}
