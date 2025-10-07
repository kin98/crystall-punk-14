using Content.Shared._CE14.Skill.Prototypes;
using Content.Shared.Damage.Components;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Skill.Effects;

public sealed partial class AddManaStamina : CE14SkillEffect
{
    [DataField]
    public float AdditionalStamina = 0;

    public override void AddSkill(IEntityManager entManager, EntityUid target)
    {
        if (!entManager.TryGetComponent<StaminaComponent>(target, out var staminaComp))
            return;

        staminaComp.CritThreshold += AdditionalStamina;
        entManager.Dirty(target, staminaComp);
    }

    public override void RemoveSkill(IEntityManager entManager, EntityUid target)
    {
        if (!entManager.TryGetComponent<StaminaComponent>(target, out var staminaComp))
            return;

        staminaComp.CritThreshold -= AdditionalStamina;
        entManager.Dirty(target, staminaComp);
    }

    public override string? GetName(IEntityManager entMagager, IPrototypeManager protoManager)
    {
        return null;
    }

    public override string? GetDescription(IEntityManager entMagager, IPrototypeManager protoManager, ProtoId<CE14SkillPrototype> skill)
    {
        return Loc.GetString("CE14-skill-desc-add-stamina", ("stamina", AdditionalStamina.ToString()));
    }
}
