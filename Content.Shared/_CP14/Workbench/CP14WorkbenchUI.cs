/*
 * This file is sublicensed under MIT License
 * https://github.com/space-wizards/space-station-14/blob/master/LICENSE.TXT
 */

using Content.Shared._CE14.Workbench.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._CE14.Workbench;

[Serializable, NetSerializable]
public enum CE14WorkbenchUiKey
{
    Key,
}

[Serializable, NetSerializable]
public sealed class CE14WorkbenchUiCraftMessage(ProtoId<CE14WorkbenchRecipePrototype> recipe)
    : BoundUserInterfaceMessage
{
    public readonly ProtoId<CE14WorkbenchRecipePrototype> Recipe = recipe;
}


[Serializable, NetSerializable]
public sealed class CE14WorkbenchUiRecipesState(List<CE14WorkbenchUiRecipesEntry> recipes) : BoundUserInterfaceState
{
    public readonly List<CE14WorkbenchUiRecipesEntry> Recipes = recipes;
}

[Serializable, NetSerializable]
public readonly struct CE14WorkbenchUiRecipesEntry(ProtoId<CE14WorkbenchRecipePrototype> protoId, bool craftable)
    : IEquatable<CE14WorkbenchUiRecipesEntry>
{
    public readonly ProtoId<CE14WorkbenchRecipePrototype> ProtoId = protoId;
    public readonly bool Craftable = craftable;

    public int CompareTo(CE14WorkbenchUiRecipesEntry other)
    {
        return Craftable.CompareTo(other.Craftable);
    }

    public override bool Equals(object? obj)
    {
        return obj is CE14WorkbenchUiRecipesEntry other && Equals(other);
    }

    public bool Equals(CE14WorkbenchUiRecipesEntry other)
    {
        return ProtoId.Id == other.ProtoId.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ProtoId, Craftable);
    }

    public override string ToString()
    {
        return $"{ProtoId} ({Craftable})";
    }

    public static int CompareTo(CE14WorkbenchUiRecipesEntry left, CE14WorkbenchUiRecipesEntry right)
    {
        return right.CompareTo(left);
    }
}
