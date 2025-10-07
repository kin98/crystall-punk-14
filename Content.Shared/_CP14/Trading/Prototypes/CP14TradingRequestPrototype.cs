using Content.Shared._CE14.Workbench;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Trading.Prototypes;

[Prototype("CE14TradingRequest")]
public sealed partial class CE14TradingRequestPrototype : IPrototype
{
    [IdDataField] public string ID { get; private set; } = default!;

    [DataField]
    public HashSet<ProtoId<CE14TradingFactionPrototype>> PossibleFactions = [];

    [DataField]
    public float GenerationWeight = 1f;

    [DataField]
    public int FromMinutes = 0;

    [DataField]
    public int? ToMinutes;

    [DataField]
    public int AdditionalReward = 10;

    [DataField]
    public float ReputationCashback = 0.015f;

    [DataField(required: true)]
    public List<CE14WorkbenchCraftRequirement> Requirements = new();
}
