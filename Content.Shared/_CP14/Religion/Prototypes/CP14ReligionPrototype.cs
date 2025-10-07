using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Religion.Prototypes;

/// <summary>
///
/// </summary>
[Prototype("CE14Religion")]
public sealed partial class CE14ReligionPrototype : IPrototype
{
    [IdDataField] public string ID { get; } = default!;

    [DataField]
    public float FollowerObservationRadius = 10f;
}
