/*
 * This file is sublicensed under MIT License
 * https://github.com/space-wizards/space-station-14/blob/master/LICENSE.TXT
 */

using Content.Shared._CE14.Skill;
using Content.Shared._CE14.Workbench;
using Content.Shared.Placeable;

namespace Content.Server._CE14.Workbench;

public sealed partial class CE14WorkbenchSystem
{
    [Dependency] private readonly CE14SharedSkillSystem _skill = default!;

    private void OnCraft(Entity<CE14WorkbenchComponent> entity, ref CE14WorkbenchUiCraftMessage args)
    {
        if (!entity.Comp.Recipes.Contains(args.Recipe))
            return;

        if (!_proto.TryIndex(args.Recipe, out var prototype))
            return;

        StartCraft(entity, args.Actor, prototype);
    }

    private void UpdateUIRecipes(Entity<CE14WorkbenchComponent> entity)
    {
        var getResource = new CE14WorkbenchGetResourcesEvent();
        RaiseLocalEvent(entity, getResource);

        var resources = getResource.Resources;

        var recipes = new List<CE14WorkbenchUiRecipesEntry>();
        foreach (var recipeId in entity.Comp.Recipes)
        {
            if (!_proto.TryIndex(recipeId, out var indexedRecipe))
                continue;

            var canCraft = true;

            foreach (var requirement in indexedRecipe.Requirements)
            {
                if (!requirement.CheckRequirement(EntityManager, _proto, resources))
                {
                    canCraft = false;
                    break;
                }
            }

            var entry = new CE14WorkbenchUiRecipesEntry(recipeId, canCraft);

            recipes.Add(entry);
        }

        _userInterface.SetUiState(entity.Owner, CE14WorkbenchUiKey.Key, new CE14WorkbenchUiRecipesState(recipes));
    }
}
