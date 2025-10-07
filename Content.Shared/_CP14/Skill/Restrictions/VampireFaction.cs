using Content.Shared._CE14.Vampire;
using Content.Shared._CE14.Vampire.Components;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Skill.Restrictions;

public sealed partial class VampireFaction : CE14SkillRestriction
{
    public override bool HideFromUI => true;

    [DataField(required: true)]
    public ProtoId<CE14VampireFactionPrototype> Clan;

    public override bool Check(IEntityManager entManager, EntityUid target)
    {
        if (!entManager.TryGetComponent<CE14VampireComponent>(target, out var vampire))
            return false;

        return vampire.Faction == Clan;
    }

    public override string GetDescription(IEntityManager entManager, IPrototypeManager protoManager)
    {
        var clan = protoManager.Index(Clan);

        return Loc.GetString("CE14-skill-req-vampire-clan", ("name", Loc.GetString(clan.Name)));
    }
}
