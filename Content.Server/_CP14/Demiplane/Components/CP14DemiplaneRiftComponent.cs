using Robust.Shared.Prototypes;

namespace Content.Server._CE14.Demiplane.Components;

/// <summary>
/// Creates a new map of the next level of the demiplane and connects to it via a portal.
/// </summary>
[RegisterComponent, Access(typeof(CE14DemiplaneSystem))]
public sealed partial class CE14DemiplaneRiftComponent : Component
{
    /// <summary>
    /// Blocks the creation of a new demiplane map, after the first one is created.
    /// </summary>
    [DataField]
    public bool CanCreate = true;

    [DataField]
    public EntProtoId AwaitingProto = "CE14DemiplaneRiftAwaiting";

    [DataField]
    public EntProtoId PortalProto = "CE14DemiplaneRiftPortal";

    [DataField]
    public EntityUid? AwaitingEntity;

    [DataField]
    public EntityUid? ScanningTargetMap;

    [DataField]
    public TimeSpan NextScanTime = TimeSpan.Zero;
}
