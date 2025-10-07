namespace Content.Server._CE14.MagicSpellStorage.Components;

/// <summary>
/// Denotes that this item's spells can be accessed while holding it in your hand
/// </summary>
[RegisterComponent, Access(typeof(CE14SpellStorageSystem))]
public sealed partial class CE14SpellStorageAccessHoldingComponent : Component
{
}
