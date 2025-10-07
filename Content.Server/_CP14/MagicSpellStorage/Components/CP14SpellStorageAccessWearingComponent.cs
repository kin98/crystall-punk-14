namespace Content.Server._CE14.MagicSpellStorage.Components;

/// <summary>
/// Denotes that this item's spells can be accessed while wearing it on your body
/// </summary>
[RegisterComponent, Access(typeof(CE14SpellStorageSystem))]
public sealed partial class CE14SpellStorageAccessWearingComponent : Component
{
    [DataField]
    public bool Wearing;
}
