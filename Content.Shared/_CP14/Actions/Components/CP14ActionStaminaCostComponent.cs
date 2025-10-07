using Content.Shared._CE14.MagicSpell;

namespace Content.Shared._CE14.Actions.Components;

/// <summary>
/// Restricts the use of this action, by spending stamina.
/// </summary>
[RegisterComponent]
public sealed partial class CE14ActionStaminaCostComponent : Component
{
    [DataField]
    public float Stamina = 0f;
}
