using Content.Shared._CE14.ModularCraft;
using Content.Shared._CE14.ModularCraft.Components;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.ModularCraft.Modifiers;

public sealed partial class AddComponents : CE14ModularCraftModifier
{
    [DataField]
    public ComponentRegistry? Components;

    [DataField]
    public bool Override = false;

    public override void Effect(EntityManager entManager, Entity<CE14ModularCraftStartPointComponent> start, Entity<CE14ModularCraftPartComponent>? part)
    {
        if (Components is not null)
            entManager.AddComponents(start, Components, Override);
    }
}
