namespace Content.Shared._CE14.MeleeWeapon.Components;

/// <summary>
/// attacks with this item may knock CE14ParriableComponent items out of your hand on a hit
/// </summary>
[RegisterComponent]
public sealed partial class CE14MeleeParryComponent : Component
{
    [DataField]
    public TimeSpan ParryWindow = TimeSpan.FromSeconds(1f);

    [DataField]
    public float ParryPower = 1f;
}
