using Content.Shared._CE14.Skill;
using Content.Shared._CE14.Skill.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellAddMemoryPoint : CE14SpellEffect
{
    [DataField]
    public float AddedPoints = 0.5f;

    [DataField]
    public float Limit = 6.5f;

    [DataField]
    public ProtoId<CE14SkillPointPrototype> SkillPointType = "Memory";

    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.Target is null)
            return;

        var skillSys = entManager.System<CE14SharedSkillSystem>();

        skillSys.AddSkillPoints(args.Target.Value, SkillPointType, AddedPoints, Limit);
    }
}
