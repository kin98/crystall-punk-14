using Content.Shared._CE14.ModularCraft.Components;
using JetBrains.Annotations;

namespace Content.Shared._CE14.ModularCraft;

[ImplicitDataDefinitionForInheritors]
[MeansImplicitUse]
public abstract partial class CE14ModularCraftModifier
{
    public abstract void Effect(EntityManager entManager, Entity<CE14ModularCraftStartPointComponent> start, Entity<CE14ModularCraftPartComponent>? part);
}
