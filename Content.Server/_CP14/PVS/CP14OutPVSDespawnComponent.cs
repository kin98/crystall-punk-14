namespace Content.Server._CE14.PVS;

/// <summary>
/// Deletes an entity if it stays out of the players' pvs for too long
/// </summary>
[RegisterComponent, Access(typeof(CE14HelperPvsSystem)), AutoGenerateComponentPause]
public sealed partial class CE14OutPVSDespawnComponent : Component
{
    [DataField]
    public int MaxDespawnAttempt = 3;

    [DataField]
    public int DespawnAttempt = 3;

    [DataField, AutoPausedField]
    public TimeSpan NextUpdate = TimeSpan.Zero;
}
