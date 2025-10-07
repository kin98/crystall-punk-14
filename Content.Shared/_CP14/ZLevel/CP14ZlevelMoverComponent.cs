using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.ZLevel;

/// <summary>
/// component that allows you to quickly move between Z levels
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class CE14ZLevelMoverComponent : Component
{
    [DataField]
    public EntProtoId UpActionProto = "CE14ActionZLevelUp";

    [DataField, AutoNetworkedField]
    public EntityUid? CE14ZLevelUpActionEntity;

    [DataField]
    public EntProtoId DownActionProto = "CE14ActionZLevelDown";

    [DataField, AutoNetworkedField]
    public EntityUid? CE14ZLevelDownActionEntity;
}
