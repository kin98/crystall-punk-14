using Content.Shared.Damage;
using Content.Shared.FixedPoint;
using Robust.Shared.GameStates;

namespace Content.Shared._CE14.MagicWeakness;

/// <summary>
/// imposes damage on excessive use of magic
/// </summary>
[RegisterComponent, NetworkedComponent]
[Access(typeof(CE14SharedMagicWeaknessSystem))]
public sealed partial class CE14MagicUnsafeDamageComponent : Component
{
    [DataField]
    public DamageSpecifier DamagePerEnergy = new()
    {
        DamageDict = new Dictionary<string, FixedPoint2>
        {
            {"CE14ManaDepletion", 0.8},
        },
    };
}
