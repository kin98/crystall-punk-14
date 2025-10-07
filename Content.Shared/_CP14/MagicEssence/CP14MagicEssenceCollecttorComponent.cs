using Robust.Shared.GameStates;

namespace Content.Shared._CE14.MagicEssence;

/// <summary>
/// Reflects the amount of essence stored in this item. The item can be destroyed to release the essence from it.
/// </summary>
[RegisterComponent, NetworkedComponent]
[Access(typeof(CE14MagicEssenceSystem))]
public sealed partial class CE14MagicEssenceCollectorComponent : Component
{
    [DataField]
    public float CollectRange = 1f;

    [DataField]
    public float AttractRange = 5f;

    [DataField]
    public string Solution = "collector";
}
