namespace Content.Shared._CE14.Actions.Components;

/// <summary>
/// Slows the caster while using action
/// </summary>
[RegisterComponent, Access(typeof(CE14SharedActionSystem))]
public sealed partial class CE14ActionDoAfterSlowdownComponent : Component
{
    [DataField]
    public float SpeedMultiplier = 1f;
}
