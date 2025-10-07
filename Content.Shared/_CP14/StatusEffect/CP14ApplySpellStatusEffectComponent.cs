using Content.Shared._CE14.MagicSpell.Spells;
using Robust.Shared.GameStates;

namespace Content.Shared._CE14.StatusEffect;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentPause]
[Access(typeof(CE14ApplySpellStatusEffectSystem))]
public sealed partial class CE14ApplySpellStatusEffectComponent : Component
{
    [DataField(serverOnly: true)]
    public List<CE14SpellEffect> StartEffect = new();

    [DataField(serverOnly: true)]
    public List<CE14SpellEffect> EndEffect = new();

    [DataField(serverOnly: true)]
    public List<CE14SpellEffect> UpdateEffect = new();

    [DataField]
    public TimeSpan UpdateFrequency = TimeSpan.FromSeconds(1f);

    [DataField, AutoPausedField]
    public TimeSpan NextUpdateTime = TimeSpan.Zero;
}
