using Content.Shared._CE14.Skill;
using Content.Shared._CE14.Skill.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellRemoveMemoryPoint : CE14SpellEffect
{
    [DataField]
    public float RemovedPoints = 0.5f;

    [DataField]
    public ProtoId<CE14SkillPointPrototype> SkillPointType = "Memory";

    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.Target is null)
            return;

        var skillSys = entManager.System<CE14SharedSkillSystem>();

        skillSys.RemoveSkillPoints(args.Target.Value, SkillPointType, RemovedPoints);
    }
}
