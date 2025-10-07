using Content.Shared._CE14.Skill.Prototypes;
using Content.Shared.Body.Prototypes;
using Content.Shared.Chemistry.Reagent;
using Content.Shared.FixedPoint;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Vampire.Components;

[RegisterComponent]
[NetworkedComponent]
[AutoGenerateComponentState]
[Access(typeof(CE14SharedVampireSystem))]
public sealed partial class CE14VampireComponent : Component
{
    [DataField]
    public ProtoId<ReagentPrototype> NewBloodReagent = "CE14BloodVampire";
    [DataField]
    public ProtoId<CE14SkillTreePrototype> SkillTreeProto = "Vampire";

    [DataField]
    public ProtoId<MetabolizerTypePrototype> MetabolizerType = "CE14Vampire";

    [DataField]
    public ProtoId<CE14SkillPointPrototype> SkillPointProto = "Blood";

    [DataField(required: true), AutoNetworkedField]
    public ProtoId<CE14VampireFactionPrototype>? Faction;

    [DataField]
    public FixedPoint2 SkillPointCount = 2f;

    [DataField]
    public TimeSpan ToggleVisualsTime = TimeSpan.FromSeconds(2f);

    /// <summary>
    /// All this actions was granted to vampires on component added
    /// </summary>
    [DataField]
    public List<EntProtoId> ActionsProto = new() { "CE14ActionVampireToggleVisuals" };

    /// <summary>
    /// For tracking granted actions, and removing them when component is removed.
    /// </summary>
    [DataField]
    public List<EntityUid> Actions = new();

    [DataField]
    public float HeatUnderSunTemperature = 12000f;

    [DataField]
    public TimeSpan HeatFrequency = TimeSpan.FromSeconds(1);

    [DataField]
    public TimeSpan NextHeatTime = TimeSpan.Zero;

    [DataField]
    public float IgniteThreshold = 350f;
}
