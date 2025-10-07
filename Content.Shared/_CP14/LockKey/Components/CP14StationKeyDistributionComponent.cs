using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.LockKey.Components;

/// <summary>
///
/// </summary>
[RegisterComponent]
public sealed partial class CE14StationKeyDistributionComponent : Component
{
    [DataField]
    public List<ProtoId<CE14LockTypePrototype>> Keys = new();
}
