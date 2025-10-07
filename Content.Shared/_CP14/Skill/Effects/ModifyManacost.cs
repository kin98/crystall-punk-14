using System.Text;
using Content.Shared._CE14.MagicManacostModify;
using Content.Shared._CE14.MagicRitual.Prototypes;
using Content.Shared._CE14.Skill.Prototypes;
using Content.Shared.FixedPoint;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Skill.Effects;

public sealed partial class ModifyManacost : CE14SkillEffect
{
    [DataField]
    public FixedPoint2 Global = 0f;

    public override void AddSkill(IEntityManager entManager, EntityUid target)
    {
        entManager.EnsureComponent<CE14MagicManacostModifyComponent>(target, out var magicEffectManaCost);

        magicEffectManaCost.GlobalModifier += Global;
    }

    public override void RemoveSkill(IEntityManager entManager, EntityUid target)
    {
        entManager.EnsureComponent<CE14MagicManacostModifyComponent>(target, out var magicEffectManaCost);

        magicEffectManaCost.GlobalModifier -= Global;
    }

    public override string? GetName(IEntityManager entMagager, IPrototypeManager protoManager)
    {
        return null;
    }

    public override string? GetDescription(IEntityManager entMagager, IPrototypeManager protoManager, ProtoId<CE14SkillPrototype> skill)
    {
        var sb = new StringBuilder();
        sb.Append(Loc.GetString("CE14-clothing-magic-examine")+"\n");

        if (Global != 0)
        {

            var plus = (float)Global > 0 ? "+" : "";
            sb.Append(
                $"{Loc.GetString("CE14-clothing-magic-global")}: {plus}{MathF.Round((float)Global * 100, MidpointRounding.AwayFromZero)}%\n");
        }

        return sb.ToString();
    }
}
