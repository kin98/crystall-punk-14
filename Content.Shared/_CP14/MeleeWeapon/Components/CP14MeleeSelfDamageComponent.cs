using Content.Shared.Damage;

namespace Content.Shared._CE14.MeleeWeapon.Components;

[RegisterComponent]
public sealed partial class CE14MeleeSelfDamageComponent : Component
{
    [DataField]
    public DamageSpecifier DamageToSelf = new()
    {
        DamageDict = new()
        {
            { "Blunt", 1 },
        }
    };
}
