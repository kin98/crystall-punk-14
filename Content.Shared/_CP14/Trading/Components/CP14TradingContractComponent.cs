using Content.Shared._CE14.Trading.Prototypes;
using Content.Shared._CE14.Trading.Systems;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Trading.Components;

[RegisterComponent, Access(typeof(CE14SharedTradingPlatformSystem))]
public sealed partial class CE14TradingContractComponent : Component
{
    [DataField]
    public ProtoId<CE14TradingFactionPrototype> Faction;
}
