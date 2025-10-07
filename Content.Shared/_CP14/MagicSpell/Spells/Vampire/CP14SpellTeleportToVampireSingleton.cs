using System.Numerics;
using Content.Shared._CE14.UniqueLoot;
using Content.Shared._CE14.Vampire.Components;
using Content.Shared.Teleportation.Systems;
using Robust.Shared.Network;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellTeleportToVampireSingleton : CE14SpellEffect
{
    [DataField]
    public EntProtoId PortalProto = "CE14TempPortalRed";

    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.Position is null)
            return;
        if (args.User is null)
            return;

        if (!entManager.TryGetComponent<CE14VampireComponent>(args.User.Value, out var vampireComponent))
            return;

        var net = IoCManager.Resolve<INetManager>();

        if (net.IsClient)
            return;

        var protoMan = IoCManager.Resolve<IPrototypeManager>();
        var random = IoCManager.Resolve<IRobustRandom>();
        var linkSys = entManager.System<LinkedEntitySystem>();
        var query = entManager.EntityQueryEnumerator<CE14SingletonComponent, TransformComponent>();

        if (!protoMan.TryIndex(vampireComponent.Faction, out var indexedVampireFaction))
            return;

        var first = entManager.SpawnAtPosition(PortalProto, args.Position.Value);

        while (query.MoveNext(out var uid, out var singleton, out var xform))
        {

            if (singleton.Key != indexedVampireFaction.SingletonTeleportKey)
                continue;

            var second = entManager.SpawnAtPosition(PortalProto, xform.Coordinates);

            linkSys.TryLink(first, second, true);
            return;
        }
    }
}
