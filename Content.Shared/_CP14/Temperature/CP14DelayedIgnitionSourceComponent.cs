using Robust.Shared.GameStates;

namespace Content.Shared._CE14.Temperature;

/// <summary>
/// Allows you to fire entities through interacting with them after a delay.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState, Access(typeof(CE14SharedFireSpreadSystem))]
public sealed partial class CE14DelayedIgnitionSourceComponent : Component
{
    [DataField, AutoNetworkedField]
    public bool Enabled;

    [DataField]
    public TimeSpan Delay = TimeSpan.FromSeconds(3f);
}
