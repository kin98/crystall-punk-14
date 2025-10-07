using Content.Shared._CE14.Religion.Components;
using Content.Shared._CE14.Religion.Prototypes;
using Content.Shared.DoAfter;
using Content.Shared.Verbs;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._CE14.Religion.Systems;

public abstract partial class CE14SharedReligionGodSystem
{
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;

    private void InitializeAltars()
    {
        SubscribeLocalEvent<CE14ReligionAltarComponent, GetVerbsEvent<AlternativeVerb>>(GetAltVerb);
    }

    private void GetAltVerb(Entity<CE14ReligionAltarComponent> ent, ref GetVerbsEvent<AlternativeVerb> args)
    {
        if (ent.Comp.Religion is null)
            return;

        var disabled = !CanBecomeFollower(args.User, ent.Comp.Religion.Value);

        if (!disabled && TryComp<CE14ReligionPendingFollowerComponent>(args.User, out var pendingFollower))
        {
            if (pendingFollower.Religion is not null)
                disabled = true;
        }

        if (disabled)
            return;

        var user = args.User;
        args.Verbs.Add(new AlternativeVerb()
        {
            Text = Loc.GetString("CE14-altar-become-follower"),
            Message = Loc.GetString("CE14-altar-become-follower-desc"),
            Act = () =>
            {
                var doAfterArgs = new DoAfterArgs(EntityManager, user, 5f, new CE14AltarOfferDoAfter(), ent, used: ent)
                {
                    BreakOnDamage = true,
                    BreakOnMove = true,
                };
                _doAfter.TryStartDoAfter(doAfterArgs);
            },
        });
    }
}

[Serializable, NetSerializable]
public sealed partial class CE14AltarOfferDoAfter : SimpleDoAfterEvent
{
}
