using Content.Shared._CE14.AuraDNA;
using Content.Shared.StatusEffectNew.Components;
using Robust.Shared.GameStates;

namespace Content.Shared._CE14.MagicVision.Components;

/// <summary>
/// Makes you leave random imprints of magical aura instead of the original
/// Use only in conjunction with <see cref="StatusEffectComponent"/>, on the status effect entity.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState, Access(typeof(CE14SharedAuraImprintSystem))]
public sealed partial class CE14HideMagicAuraStatusEffectComponent : Component
{
    [DataField, AutoNetworkedField]
    public string Imprint = string.Empty;
}
