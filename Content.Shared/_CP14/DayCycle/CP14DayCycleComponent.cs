namespace Content.Shared._CE14.DayCycle;


[RegisterComponent]
public sealed partial class CE14DayCycleComponent : Component
{
    public float LastLightLevel = 0f;

    [DataField]
    public float Threshold = 0.6f;
}
