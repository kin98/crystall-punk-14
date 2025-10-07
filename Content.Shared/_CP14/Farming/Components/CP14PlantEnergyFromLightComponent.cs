namespace Content.Shared._CE14.Farming.Components;

/// <summary>
/// allows the plant to receive energy passively, depending on daylight
/// </summary>
[RegisterComponent, Access(typeof(CE14SharedFarmingSystem))]
public sealed partial class CE14PlantEnergyFromLightComponent : Component
{
    [DataField]
    public float Energy = 0f;

    [DataField]
    public bool Daytime = true;

    [DataField]
    public bool Nighttime = false;
}
