using Content.Shared.Damage.Prototypes;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.StatusEffect;

[RegisterComponent, NetworkedComponent]
[Access(typeof(CE14ApplySpellStatusEffectSystem))]
public sealed partial class CE14DamageModifierStatusEffectComponent : Component
{
    [DataField]
    public Dictionary<ProtoId<DamageTypePrototype>, float>? Defence = null;

    [DataField]
    public float GlobalDefence = 1f;
}
