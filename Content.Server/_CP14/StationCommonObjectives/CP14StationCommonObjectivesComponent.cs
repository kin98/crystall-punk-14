using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.StationCommonObjectives;

[RegisterComponent]
public sealed partial class CE14StationCommonObjectivesComponent : Component
{
    public Dictionary<EntityUid, ProtoId<JobPrototype>> JobObjectives = new();
    public Dictionary<EntityUid, ProtoId<DepartmentPrototype>> DepartmentObjectives = new();
}
