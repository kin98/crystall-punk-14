namespace Content.Client._CE14.Wave;

[RegisterComponent]
[Access(typeof(CE14WaveShaderSystem))]
public sealed partial class CE14WaveShaderComponent : Component
{
    [DataField]
    public float Speed = 10f;

    [DataField]
    public float Dis = 10f;

    [DataField]
    public float Offset = 0f;
}
