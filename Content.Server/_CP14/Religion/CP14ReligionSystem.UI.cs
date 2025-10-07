using Content.Shared._CE14.MagicEnergy.Components;
using Content.Shared._CE14.Religion.Components;
using Content.Shared._CE14.Religion.Systems;
using Content.Shared.FixedPoint;
using Content.Shared.Follower;
using Robust.Server.GameObjects;

namespace Content.Server._CE14.Religion;

public sealed partial class CE14ReligionGodSystem
{
    [Dependency] private readonly UserInterfaceSystem _userInterface = default!;
    [Dependency] private readonly FollowerSystem _follower = default!;
    private void InitializeUI()
    {
        SubscribeLocalEvent<CE14ReligionEntityComponent, OpenBoundInterfaceMessage>(OnOpenInterface);
        SubscribeLocalEvent<CE14ReligionEntityComponent, CE14ReligionEntityTeleportAttempt>(OnTeleportAttempt);
    }

    private void OnTeleportAttempt(Entity<CE14ReligionEntityComponent> ent, ref CE14ReligionEntityTeleportAttempt args)
    {
        var target = GetEntity(args.Entity);

        var canTeleport = false;
        if (TryComp<CE14ReligionAltarComponent>(target, out var altar))
        {
            if (altar.Religion == ent.Comp.Religion)
                canTeleport = true;
        }
        else if (TryComp<CE14ReligionFollowerComponent>(target, out var follower))
        {
            if (follower.Religion == ent.Comp.Religion)
                canTeleport = true;
        }

        if (!canTeleport)
            return;

        _follower.StartFollowingEntity(ent, target);
    }

    private void OnOpenInterface(Entity<CE14ReligionEntityComponent> ent, ref OpenBoundInterfaceMessage args)
    {
        if (ent.Comp.Religion is null)
            return;

        var altars = new Dictionary<NetEntity, string>();
        var queryAltars = EntityQueryEnumerator<CE14ReligionAltarComponent, MetaDataComponent>();
        while (queryAltars.MoveNext(out var uid, out var altar, out var meta))
        {
            if (altar.Religion != ent.Comp.Religion)
                continue;

            altars.TryAdd(GetNetEntity(uid), meta.EntityName);
        }

        var followers = new Dictionary<NetEntity, string>();
        var queryFollowers = EntityQueryEnumerator<CE14ReligionFollowerComponent, MetaDataComponent>();
        while (queryFollowers.MoveNext(out var uid, out var follower, out var meta))
        {
            if (follower.Religion != ent.Comp.Religion)
                continue;

            followers.TryAdd(GetNetEntity(uid), meta.EntityName);
        }

        var followerPercentage = GetFollowerPercentage(ent);
        ent.Comp.FollowerPercentage = followerPercentage;
        Dirty(ent);

        FixedPoint2 manaPercentage = 0f;
        if (TryComp<CE14MagicEnergyContainerComponent>(ent, out var manaContainerComponent))
        {
            manaPercentage = manaContainerComponent.Energy / manaContainerComponent.MaxEnergy;
        }

        _userInterface.SetUiState(ent.Owner, CE14ReligionEntityUiKey.Key, new CE14ReligionEntityUiState(altars, followers, followerPercentage, manaPercentage));
    }
}
