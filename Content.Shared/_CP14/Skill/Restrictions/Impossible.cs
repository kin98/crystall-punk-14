using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Skill.Restrictions;

public sealed partial class Impossible : CE14SkillRestriction
{
    public override bool Check(IEntityManager entManager, EntityUid target)
    {
        return false;
    }

    public override string GetDescription(IEntityManager entManager, IPrototypeManager protoManager)
    {
        return Loc.GetString("CE14-skill-req-impossible");
    }
}
