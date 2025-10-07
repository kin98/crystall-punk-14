using Content.Shared._CE14.Skill.Components;
using Content.Shared._CE14.Skill.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Skill.Restrictions;

public sealed partial class NeedPrerequisite : CE14SkillRestriction
{
    [DataField(required: true)]
    public ProtoId<CE14SkillPrototype> Prerequisite = new();

    public override bool Check(IEntityManager entManager, EntityUid target)
    {
        if (!entManager.TryGetComponent<CE14SkillStorageComponent>(target, out var skillStorage))
            return false;

        var learned = skillStorage.LearnedSkills;
        return learned.Contains(Prerequisite);
    }

    public override string GetDescription(IEntityManager entManager, IPrototypeManager protoManager)
    {
        var skillSystem = entManager.System<CE14SharedSkillSystem>();

        return Loc.GetString("CE14-skill-req-prerequisite", ("name", skillSystem.GetSkillName(Prerequisite)));
    }
}
