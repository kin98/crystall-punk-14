namespace Content.Server._CE14.Temperature;

/// <summary>
/// passively returns the solution temperature to the standard
/// </summary>
[RegisterComponent, Access(typeof(CE14TemperatureSystem))]
public sealed partial class CE14SolutionTemperatureComponent : Component
{
    [DataField]
    public float StandardTemp = 300f;
}
