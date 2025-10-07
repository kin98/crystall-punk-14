namespace Content.Server._CE14.Temperature;

/// <summary>
/// allows you to heat the temperature of solutions depending on the number of stacks of fire
/// </summary>
[RegisterComponent, Access(typeof(CE14TemperatureSystem))]
public sealed partial class CE14FlammableSolutionHeaterComponent : Component
{
    [DataField]
    public float DegreesPerStack = 100f;
}
