namespace Content.Client._CE14.Farming;

/// <summary>
/// Controls the visual display of plant growth
/// </summary>
[RegisterComponent]
public sealed partial class CE14PlantVisualsComponent : Component
{
    [DataField]
    public int GrowthSteps = 3;

    [DataField]
    public string? GrowState;

    [DataField]
    public string? GrowUnshadedState;
}

public enum PlantVisualLayers : byte
{
    Base,
    BaseUnshaded,
}
