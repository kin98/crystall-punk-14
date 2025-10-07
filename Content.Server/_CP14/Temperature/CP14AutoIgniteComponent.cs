namespace Content.Server._CE14.Temperature;

[RegisterComponent, AutoGenerateComponentPause, Access(typeof(CE14FireSpreadSystem))]
public sealed partial class CE14AutoIgniteComponent : Component
{
    [DataField]
    public float StartStack = 1f;

    [DataField]
    public TimeSpan IgniteDelay = TimeSpan.FromSeconds(1f);

    [DataField]
    public TimeSpan IgniteTime = TimeSpan.Zero;
}
