using Content.Shared._CE14.Skill.Prototypes;
using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Skill;

public sealed partial class CE14LearnSkillsSpecial : JobSpecial
{
    [DataField]
    public HashSet<ProtoId<CE14SkillPrototype>> Skills { get; private set; } = new();

    public override void AfterEquip(EntityUid mob)
    {
        var entMan = IoCManager.Resolve<IEntityManager>();
        var skillSys = entMan.System<CE14SharedSkillSystem>();

        foreach (var skill in Skills)
        {
            skillSys.TryAddSkill(mob, skill, free: true);
        }
    }
}
