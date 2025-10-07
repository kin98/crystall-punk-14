namespace Content.Server._CE14.GameTicking.Rules.Components;

/// <summary>
/// A rule that assigns common goals to different roles. Common objectives are generated once at the beginning of a round and are shared between players.
/// </summary>
[RegisterComponent, Access(typeof(CE14CrashingShipRule))]
public sealed partial class CE14CrashingShipRuleComponent : Component
{
    [DataField]
    public EntityUid? Ship;

    [DataField]
    public bool PendingExplosions = true;

    [DataField]
    public TimeSpan StartExplosionTime = TimeSpan.FromMinutes(1);
}
