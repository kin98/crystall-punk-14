using Content.Shared._CE14.ModularCraft;
using Content.Shared._CE14.ModularCraft.Components;
using Content.Shared.Armor;
using Content.Shared.Clothing;
using Content.Shared.Damage;

namespace Content.Server._CE14.ModularCraft.Modifiers;

public sealed partial class EditClothingSpeed : CE14ModularCraftModifier
{
    [DataField]
    public float WalkModifier = 1f;

    [DataField]
    public float SprintModifier = 1f;

    public override void Effect(EntityManager entManager, Entity<CE14ModularCraftStartPointComponent> start, Entity<CE14ModularCraftPartComponent>? part)
    {
        if (!entManager.TryGetComponent<ClothingSpeedModifierComponent>(start, out var speed))
            return;

        var speedModifierSystem = entManager.System<ClothingSpeedModifierSystem>();

        speedModifierSystem.EditSpeedModifiers(start, WalkModifier, SprintModifier, speed);
    }
}
