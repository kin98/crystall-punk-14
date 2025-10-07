using System.Linq;
using Content.Shared._CE14.MeleeWeapon.Components;
using Content.Shared.Damage;
using Content.Shared.Examine;
using Content.Shared.Interaction;
using Content.Shared.Placeable;
using Content.Shared.Popups;
using Content.Shared.Timing;
using Content.Shared.Weapons.Melee.Events;
using Content.Shared.Wieldable;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Network;

namespace Content.Shared._CE14.MeleeWeapon.EntitySystems;

public sealed class CE14SharpeningSystem : EntitySystem
{
    [Dependency] private readonly DamageableSystem _damageableSystem = default!;
    [Dependency] private readonly UseDelaySystem _useDelay = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly INetManager _net = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14SharpenedComponent, GetMeleeDamageEvent>(OnGetMeleeDamage,
            after: [typeof(SharedWieldableSystem)]);
        SubscribeLocalEvent<CE14SharpenedComponent, ExaminedEvent>(OnExamined);
        SubscribeLocalEvent<CE14SharpenedComponent, MeleeHitEvent>(OnMeleeHit);

        SubscribeLocalEvent<CE14SharpeningStoneComponent, AfterInteractEvent>(OnAfterInteract);
        SubscribeLocalEvent<CE14SharpeningStoneComponent, ActivateInWorldEvent>(OnInteract);
    }

    public static void ReduceSharpness(Entity<CE14SharpenedComponent> ent, DamageSpecifier dmg)
    {
        ent.Comp.Sharpness =
            MathHelper.Clamp(ent.Comp.Sharpness - dmg.GetTotal().Float() * ent.Comp.SharpnessDamageBy1Damage, 0.1f, 1f);
    }

    private static void OnMeleeHit(Entity<CE14SharpenedComponent> sharpened, ref MeleeHitEvent args)
    {
        if (!args.HitEntities.Any())
            return;

        ReduceSharpness(sharpened, args.BaseDamage);
    }

    private void OnInteract(Entity<CE14SharpeningStoneComponent> stone, ref ActivateInWorldEvent args)
    {
        if (args.Handled)
            return;

        if (!TryComp<ItemPlacerComponent>(stone, out var itemPlacer))
            return;

        if (itemPlacer.PlacedEntities.Count <= 0)
            return;

        foreach (var item in itemPlacer.PlacedEntities)
        {
            if (!TryComp<CE14SharpenedComponent>(item, out var sharpened))
                continue;

            SharpThing(stone, item, sharpened, args.User);
            return;
        }
    }

    private void OnAfterInteract(Entity<CE14SharpeningStoneComponent> stone, ref AfterInteractEvent args)
    {
        if (!args.CanReach || args.Target == null || !TryComp<CE14SharpenedComponent>(args.Target, out var sharpened))
            return;

        if (TryComp<UseDelayComponent>(stone, out var useDelay) && _useDelay.IsDelayed((stone, useDelay)))
            return;

        SharpThing(stone, args.Target.Value, sharpened, args.User);
    }

    private void SharpThing(Entity<CE14SharpeningStoneComponent> stone,
        EntityUid target,
        CE14SharpenedComponent component,
        EntityUid user)
    {
        var ev = new SharpingEvent
        {
            User = user,
            Target = target,
        };
        RaiseLocalEvent(stone, ev);

        if (!ev.Canceled)
        {
            _audio.PlayPredicted(stone.Comp.SharpeningSound, target, user);

            _damageableSystem.TryChangeDamage(stone, stone.Comp.SelfDamage);
            _damageableSystem.TryChangeDamage(target, stone.Comp.TargetDamage);

            component.Sharpness = MathHelper.Clamp01(component.Sharpness + stone.Comp.SharpnessHeal);

            if (_net.IsServer)
            {
                Spawn("EffectSparks", Transform(target).Coordinates);
                if (component.Sharpness >= 0.99)
                    _popup.PopupEntity(Loc.GetString("sharpening-ready"), target, user);
            }
        }

        _useDelay.TryResetDelay(stone);
    }

    private void OnExamined(Entity<CE14SharpenedComponent> sharpened, ref ExaminedEvent args)
    {
        foreach (var (threshold, locString) in sharpened.Comp.SharpnessExamineThresholds.OrderByDescending(x => x.Key))
        {
            if (!(sharpened.Comp.Sharpness > threshold) && threshold != 0)
                continue;
            args.PushMarkup(Loc.GetString(locString));
            return;
        }
    }

    private static void OnGetMeleeDamage(Entity<CE14SharpenedComponent> sharpened, ref GetMeleeDamageEvent args)
    {
        var slashDamage = args.Damage.DamageDict.GetValueOrDefault("Slash");
        var piercingDamage = args.Damage.DamageDict.GetValueOrDefault("Piercing");

        args.Damage.DamageDict["Slash"] = slashDamage * sharpened.Comp.Sharpness;
        args.Damage.DamageDict["Piercing"] = piercingDamage * sharpened.Comp.Sharpness;
        args.Damage.DamageDict["Blunt"] = (slashDamage + piercingDamage) / 2 * (1f - sharpened.Comp.Sharpness);
    }
}

/// <summary>
/// Caused on a sharpening stone when someone tries to sharpen an object with it
/// </summary>
public sealed class SharpingEvent : EntityEventArgs
{
    public bool Canceled = false;
    public EntityUid User;
    public EntityUid Target;
}
