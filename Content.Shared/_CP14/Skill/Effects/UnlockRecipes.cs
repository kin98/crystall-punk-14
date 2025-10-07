using System.Text;
using Content.Shared._CE14.Skill.Prototypes;
using Content.Shared._CE14.Workbench.Prototypes;
using Content.Shared._CE14.Workbench.Requirements;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Skill.Effects;

/// <summary>
/// This effect only exists for parsing the description.
/// </summary>
public sealed partial class UnlockRecipes : CE14SkillEffect
{
    public override void AddSkill(IEntityManager entManager, EntityUid target)
    {
        //
    }

    public override void RemoveSkill(IEntityManager entManager, EntityUid target)
    {
        //
    }

    public override string? GetName(IEntityManager entMagager, IPrototypeManager protoManager)
    {
        return null;
    }

    public override string? GetDescription(IEntityManager entMagager, IPrototypeManager protoManager, ProtoId<CE14SkillPrototype> skill)
    {
        var allRecipes = protoManager.EnumeratePrototypes<CE14WorkbenchRecipePrototype>();

        var sb = new StringBuilder();
        sb.Append(Loc.GetString("CE14-skill-desc-unlock-recipes") + "\n");

        var affectedRecipes = new List<CE14WorkbenchRecipePrototype>();
        foreach (var recipe in allRecipes)
        {
            foreach (var req in recipe.RequiredSkills)
            {
                if (req == skill)
                {
                    affectedRecipes.Add(recipe);
                    break;
                }
            }
        }
        foreach (var recipe in affectedRecipes)
        {
            if (!protoManager.TryIndex(recipe.Result, out var indexedResult))
                continue;
            sb.Append("- " + indexedResult.Name + "\n");
        }

        return sb.ToString();
    }
}
