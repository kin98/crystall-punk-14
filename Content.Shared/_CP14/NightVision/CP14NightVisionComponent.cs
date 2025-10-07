using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.NightVision;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class CE14NightVisionComponent : Component
{
    [DataField]
    public EntityUid? LocalLightEntity = null;

    [DataField, AutoNetworkedField]
    public EntProtoId LightPrototype = "CE14NightVisionLight";

    [DataField, AutoNetworkedField]
    public EntProtoId ActionPrototype = "CE14ActionToggleNightVision";

    [DataField]
    public EntityUid? ActionEntity = null;
}
