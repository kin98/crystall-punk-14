using Robust.Shared.GameStates;

namespace Content.Shared._CE14.Actions.Components;

/// <summary>
/// apply slowdown effect from casting spells
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState, Access(typeof(CE14SharedActionSystem))]
public sealed partial class CE14SlowdownFromActionsComponent : Component
{
    [DataField, AutoNetworkedField]
    public Dictionary<NetEntity, float> SpeedAffectors = new();
}
