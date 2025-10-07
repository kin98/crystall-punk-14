using Content.Shared._CE14.MagicEnergy;
using Content.Shared._CE14.Skill.Prototypes;
using Content.Shared.FixedPoint;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Skill.Effects;

public sealed partial class AddManaMax : CE14SkillEffect
{
    [DataField]
    public FixedPoint2 AdditionalMana = 0;
    public override void AddSkill(IEntityManager entManager, EntityUid target)
    {
        var magicSystem = entManager.System<CE14SharedMagicEnergySystem>();
        magicSystem.ChangeMaximumEnergy(target, AdditionalMana);
    }

    public override void RemoveSkill(IEntityManager entManager, EntityUid target)
    {
        var magicSystem = entManager.System<CE14SharedMagicEnergySystem>();
        magicSystem.ChangeMaximumEnergy(target, -AdditionalMana);
    }

    public override string? GetName(IEntityManager entMagager, IPrototypeManager protoManager)
    {
        return null;
    }

    public override string? GetDescription(IEntityManager entMagager, IPrototypeManager protoManager, ProtoId<CE14SkillPrototype> skill)
    {
        return Loc.GetString("CE14-skill-desc-add-mana", ("mana", AdditionalMana.ToString()));
    }
}
