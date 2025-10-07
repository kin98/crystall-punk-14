using Content.Shared._CE14.Damageable;
using Content.Shared._CE14.ModularCraft;
using Content.Shared._CE14.ModularCraft.Components;

namespace Content.Server._CE14.ModularCraft.Modifiers;

public sealed partial class EditDamageableModifier : CE14ModularCraftModifier
{
    [DataField(required: true)]
    public float Multiplier = 1f;

    public override void Effect(EntityManager entManager, Entity<CE14ModularCraftStartPointComponent> start, Entity<CE14ModularCraftPartComponent>? part)
    {
        var damageable = entManager.EnsureComponent<CE14DamageableModifierComponent>(start);

        damageable.Modifier *= Multiplier;
        entManager.Dirty(start);
    }
}
