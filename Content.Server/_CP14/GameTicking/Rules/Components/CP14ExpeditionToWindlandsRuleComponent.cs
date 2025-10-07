using Content.Shared._CE14.Procedural.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.GameTicking.Rules.Components;

/// <summary>
/// A rule that assigns common goals to different roles. Common objectives are generated once at the beginning of a round and are shared between players.
/// </summary>
[RegisterComponent, Access(typeof(CE14ExpeditionToWindlandsRule))]
public sealed partial class CE14ExpeditionToWindlandsRuleComponent : Component
{
    [DataField]
    public ProtoId<CE14ProceduralLocationPrototype> Location = "T1GrasslandIsland";

    [DataField]
    public List<ProtoId<CE14ProceduralModifierPrototype>> Modifiers = [];

    [DataField]
    public float FloatingTime = 120;
}
