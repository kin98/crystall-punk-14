using Content.Shared._CE14.ModularCraft;
using Content.Shared._CE14.ModularCraft.Components;
using Content.Shared._CE14.ModularCraft.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.ModularCraft.Modifiers;

public sealed partial class EditModularSlots : CE14ModularCraftModifier
{
    [DataField]
    public HashSet<ProtoId<CE14ModularCraftSlotPrototype>> AddSlots = new();

    [DataField]
    public HashSet<ProtoId<CE14ModularCraftSlotPrototype>> RemoveSlots = new();

    public override void Effect(EntityManager entManager, Entity<CE14ModularCraftStartPointComponent> start, Entity<CE14ModularCraftPartComponent>? part)
    {
        start.Comp.FreeSlots.AddRange(AddSlots);
        foreach (var slot in RemoveSlots)
        {
            if (start.Comp.FreeSlots.Contains(slot))
                start.Comp.FreeSlots.Remove(slot);
        }
    }
}
