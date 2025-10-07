using Content.Shared._CE14.ModularCraft.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.ModularCraft.Components;

/// <summary>
/// Adds all details to the item when initializing. This is useful for spawning modular items directly when mapping or as loot in dungeons
/// </summary>
[RegisterComponent, Access(typeof(CE14SharedModularCraftSystem))]
public sealed partial class CE14ModularCraftAutoAssembleComponent : Component
{
    [DataField]
    public List<EntProtoId> Details = new();
}
