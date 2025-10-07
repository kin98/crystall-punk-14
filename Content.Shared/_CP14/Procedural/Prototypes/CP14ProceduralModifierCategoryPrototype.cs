using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Procedural.Prototypes;

/// <summary>
///
/// </summary>
[Prototype("CE14LocationModifierCategory")]
public sealed partial class CE14ProceduralModifierCategoryPrototype : IPrototype
{
    [IdDataField] public string ID { get; } = default!;
}
