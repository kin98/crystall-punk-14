using Robust.Shared.GameStates;

namespace Content.Shared._CE14.AuraDNA;

/// <summary>
/// A component that stores a “blueprint” of the aura, unique to each mind.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState, Access(typeof(CE14SharedAuraImprintSystem))]
public sealed partial class CE14AuraImprintComponent : Component
{
    [DataField, AutoNetworkedField]
    public string Imprint = string.Empty;

    [DataField]
    public int ImprintLength = 8;

    [DataField]
    public Color ImprintColor = Color.White;
}
