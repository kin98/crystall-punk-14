using Content.Shared._CE14.Farming.Prototypes;
using Content.Shared.FixedPoint;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Farming.Components;

/// <summary>
/// allows the plant to obtain resources by absorbing liquid from the ground
/// </summary>
[RegisterComponent, Access(typeof(CE14SharedFarmingSystem))]
public sealed partial class CE14PlantMetabolizerComponent : Component
{
    [DataField]
    public FixedPoint2 SolutionPerUpdate = 5f;

    [DataField(required: true)]
    public ProtoId<CE14PlantMetabolizerPrototype> MetabolizerId;
}
