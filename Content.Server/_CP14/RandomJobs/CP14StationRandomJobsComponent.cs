
using Content.Shared.Destructible.Thresholds;
using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.RandomJobs;

[RegisterComponent, Access(typeof(CE14StationRandomJobsSystem))]
public sealed partial class CE14StationRandomJobsComponent : Component
{
    [DataField]
    public List<CE14RandomJobEntry> Entries = new();
}

[Serializable, DataDefinition]
public sealed partial class CE14RandomJobEntry
{
    [DataField(required: true)]
    public List<ProtoId<JobPrototype>> Jobs = new();

    [DataField(required: true)]
    public MinMax Count = new(1, 1);

    [DataField]
    public float Prob = 1f;
}
