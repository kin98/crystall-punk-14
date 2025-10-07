using Content.Shared._CE14.Trading.Prototypes;
using Content.Shared._CE14.Trading.Systems;
using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Trading;

public sealed partial class CE14AddTradingReputationSpecial : JobSpecial
{
    [DataField]
    public float Reputation = 1f;

    [DataField]
    public HashSet<ProtoId<CE14TradingFactionPrototype>> Factions = new();

    public override void AfterEquip(EntityUid mob)
    {
        var entMan = IoCManager.Resolve<IEntityManager>();
        var tradeSys = entMan.System<CE14SharedTradingPlatformSystem>();

        foreach (var faction in Factions)
        {
            tradeSys.AddReputation(mob, faction, Reputation);
        }
    }
}
