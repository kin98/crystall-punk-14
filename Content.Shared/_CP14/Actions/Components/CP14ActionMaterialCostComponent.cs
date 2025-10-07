using Content.Shared._CE14.Workbench;

namespace Content.Shared._CE14.Actions.Components;

/// <summary>
/// Requires the caster to hold a specific resource in their hand, which will be spent to use the spell.
/// </summary>
[RegisterComponent]
public sealed partial class CE14ActionMaterialCostComponent : Component
{
    [DataField(required: true)]
    public CE14WorkbenchCraftRequirement? Requirement;
}
