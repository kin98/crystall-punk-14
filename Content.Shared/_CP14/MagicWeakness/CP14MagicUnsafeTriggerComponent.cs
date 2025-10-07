using Robust.Shared.GameStates;

namespace Content.Shared._CE14.MagicWeakness;

/// <summary>
/// trigger entity on unsafe magic energy damage
/// </summary>
[RegisterComponent, NetworkedComponent]
[Access(typeof(CE14SharedMagicWeaknessSystem))]
public sealed partial class CE14MagicUnsafeTriggerComponent : Component
{
}
