using Content.Shared._CE14.Skill.Prototypes;
using Content.Shared.FixedPoint;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Actions.Components;

/// <summary>
/// Restricts the use of this action, by spending user skillpoints
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class CE14ActionSkillPointCostComponent : Component
{
    [DataField(required: true)]
    public ProtoId<CE14SkillPointPrototype>? SkillPoint;

    [DataField]
    public FixedPoint2 Count = 1f;
}
