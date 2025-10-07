using Content.Shared._CE14.Procedural.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.Procedural;

/// <summary>
/// Generates the surrounding procedural world on the game map, surrounding the mapped settlement.
/// </summary>
[RegisterComponent, Access(typeof(CE14LocationGenerationSystem))]
public sealed partial class CE14StationProceduralLocationComponent : Component
{
    [DataField(required: true)]
    public ProtoId<CE14ProceduralLocationPrototype> Location;

    [DataField]
    public List<ProtoId<CE14ProceduralModifierPrototype>> Modifiers = [];

    [DataField]
    public Vector2i GenerationOffset = Vector2i.Zero;
}
