using Content.Shared.Damage;

namespace Content.Shared._CE14.Temperature;

/// <summary>
/// Add bonus damage to melee attacks per flammable stack
/// </summary>
[RegisterComponent, Access(typeof(CE14SharedFireSpreadSystem))]
public sealed partial class CE14FlammableBonusDamageComponent : Component
{
    [DataField]
    public DamageSpecifier DamagePerStack = new()
    {
        DamageDict = new()
        {
            {"Heat", 0.3},
        }
    };
}
