using Content.Shared._CE14.Skill.Prototypes;
using Content.Shared._CE14.Vampire.Components;
using Content.Shared.CCVar;
using Content.Shared.Humanoid;
using Content.Shared.Humanoid.Prototypes;
using Robust.Shared.Configuration;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;

namespace Content.Shared._CE14.Skill.Restrictions;

public sealed partial class VampireClanLevel : CE14SkillRestriction
{
    [DataField]
    public int Level = 1;

    public override bool Check(IEntityManager entManager, EntityUid target)
    {
        if (!entManager.TryGetComponent<CE14VampireComponent>(target, out var playerVampire))
            return false;

        if (!entManager.TryGetComponent<TransformComponent>(target, out var xform))
            return false;

        var lookup = entManager.System<EntityLookupSystem>();

        foreach (var tree in lookup.GetEntitiesInRange<CE14VampireClanHeartComponent>(xform.Coordinates, 2))
        {
            if (tree.Comp.Faction != playerVampire.Faction)
                continue;

            if (tree.Comp.Level < Level)
                continue;

            return true;
        }

        return false;
    }

    public override string GetDescription(IEntityManager entManager, IPrototypeManager protoManager)
    {
        return Loc.GetString("CE14-skill-req-vampire-tree-level", ("lvl", Level));
    }
}
