using Robust.Shared.GameStates;

namespace Content.Shared._CE14.MagicEssence;

/// <summary>
/// Reflects the amount of essence stored in this item. The item can be destroyed to release the essence from it.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class CE14MagicEssenceAttractorComponent : Component
{
}
