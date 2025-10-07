using System.Numerics;
using Content.Shared._CE14.UniqueLoot;
using Content.Shared.Teleportation.Systems;
using Robust.Shared.Network;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellTeleportToSingleton : CE14SpellEffect
{
    [DataField]
    public EntProtoId PortalProto = "CE14TempPortalRed";

    [DataField(required: true)]
    public string SingletonKey;

    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.Position is null)
            return;

        var net = IoCManager.Resolve<INetManager>();

        if (net.IsClient)
            return;

        var random = IoCManager.Resolve<IRobustRandom>();
        var linkSys = entManager.System<LinkedEntitySystem>();
        var query = entManager.EntityQueryEnumerator<CE14SingletonComponent, TransformComponent>();

        var first = entManager.SpawnAtPosition(PortalProto, args.Position.Value);

        while (query.MoveNext(out var uid, out var singleton, out var xform))
        {
            if (singleton.Key != SingletonKey)
                continue;

            var randomOffset = new Vector2(random.Next(-1, 1), random.Next(-1, 1));
            var second = entManager.SpawnAtPosition(PortalProto, xform.Coordinates.Offset(randomOffset));

            linkSys.TryLink(first, second, true);
            return;
        }
    }
}
