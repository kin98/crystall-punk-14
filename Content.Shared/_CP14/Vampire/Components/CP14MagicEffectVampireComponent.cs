using Content.Shared._CE14.MagicSpell;

namespace Content.Shared._CE14.Vampire.Components;

/// <summary>
/// Use is only available if the vampire is in a “visible” dangerous form.
/// </summary>
[RegisterComponent, Access(typeof(CE14SharedVampireSystem))]
public sealed partial class CE14MagicEffectVampireComponent : Component
{
}
