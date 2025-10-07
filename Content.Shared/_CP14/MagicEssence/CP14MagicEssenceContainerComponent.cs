using Content.Shared._CE14.MagicRitual.Prototypes;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.MagicEssence;

/// <summary>
/// Reflects the amount of essence stored in this item. The item can be destroyed to release the essence from it.
/// </summary>
[RegisterComponent, NetworkedComponent]
[Access(typeof(CE14MagicEssenceSystem))]
public sealed partial class CE14MagicEssenceContainerComponent : Component
{
    [DataField]
    public Dictionary<ProtoId<CE14MagicTypePrototype>, int> Essences = new();
}
