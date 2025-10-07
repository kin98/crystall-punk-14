using Content.Shared.Whitelist;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.MagicEssence;

/// <summary>
///
/// </summary>
[RegisterComponent, NetworkedComponent]
[Access(typeof(CE14MagicEssenceSystem))]
public sealed partial class CE14MagicEssenceSplitterComponent : Component
{
    [DataField]
    public EntProtoId ImpactEffect = "CE14EssenceSplitterImpactEffect";

    [DataField]
    public float ThrowForce = 10f;

    [DataField]
    public EntityWhitelist? Whitelist;
}
