using Robust.Shared.Prototypes;

namespace Content.Server._CE14.GameTicking.Rules.Components;

/// <summary>
///When attached to shuttle, start firebombing it until FTL ends.
/// </summary>
[RegisterComponent, Access(typeof(CE14CrashingShipRule))]
public sealed partial class CE14CrashingShipComponent : Component
{
    [DataField]
    public TimeSpan NextExplosionTime = TimeSpan.Zero;

    [DataField]
    public EntProtoId ExplosionProto = "CE14ShipExplosion";

    [DataField]
    public EntProtoId FinalExplosionProto = "CE14ShipExplosionBig";
}
