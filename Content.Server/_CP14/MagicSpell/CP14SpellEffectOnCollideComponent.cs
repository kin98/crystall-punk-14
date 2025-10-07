using Content.Shared._CE14.MagicSpell.Spells;
using Content.Shared.Whitelist;

namespace Content.Server._CE14.MagicSpell;

/// <summary>
/// Component that allows an meleeWeapon to apply effects to other entities on melee attacks.
/// </summary>
[RegisterComponent]
public sealed partial class CE14SpellEffectOnCollideComponent : Component
{
    [DataField(required: true, serverOnly: true)]
    public List<CE14SpellEffect> Effects = new();

    [DataField]
    public EntityWhitelist? Whitelist;

    [DataField]
    public float Prob = 1f;
}
