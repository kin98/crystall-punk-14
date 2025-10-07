/*
 * This file is sublicensed under MIT License
 * https://github.com/space-wizards/space-station-14/blob/master/LICENSE.TXT
 */

using Content.Shared.Chemistry.Components;
using Content.Shared.Tag;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Cooking.Requirements;

public sealed partial class TagBlocked : CE14CookingCraftRequirement
{
    [DataField(required: true)]
    public HashSet<ProtoId<TagPrototype>> Tags = default!;

    public override bool CheckRequirement(IEntityManager entManager,
        IPrototypeManager protoManager,
        List<ProtoId<TagPrototype>> placedTags,
        Solution? solution = null)
    {
        foreach (var placedTag in placedTags)
        {
            if (Tags.Contains(placedTag))
                return false;
        }

        return true;
    }

    public override float GetComplexity()
    {
        return Tags.Count * -1;
    }
}
