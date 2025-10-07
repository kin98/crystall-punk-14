using Content.Shared.Damage;

namespace Content.Server._CE14.MagicSpellStorage.Components;

/// <summary>
/// Causes damage to the Spell storage when spells from it are used
/// </summary>
[RegisterComponent, Access(typeof(CE14SpellStorageSystem))]
public sealed partial class CE14SpellStorageUseDamageComponent : Component
{
    /// <summary>
    /// the amount of damage this entity will take per unit manacost of the spell used
    /// </summary>
    [DataField(required: true)]
    public DamageSpecifier DamagePerMana = default!;
}
