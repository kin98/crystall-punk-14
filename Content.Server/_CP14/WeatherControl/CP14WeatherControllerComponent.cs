using Content.Server._CE14.GameTicking.Rules;
using Content.Shared._CE14.WeatherControl;

namespace Content.Server._CE14.WeatherControl;

/// <summary>
/// is the controller that hangs on the prototype map. It regulates which weather rules are run and where they are run.
/// </summary>
[RegisterComponent, AutoGenerateComponentPause, Access(typeof(CE14WeatherControllerSystem), typeof(CE14WeatherRule))]
public sealed partial class CE14WeatherControllerComponent : Component
{
    [DataField]
    public bool Enabled = true;

    [DataField]
    public HashSet<CE14WeatherData> Entries = new();

    [DataField, AutoPausedField]
    public TimeSpan NextWeatherTime = TimeSpan.Zero;
}
