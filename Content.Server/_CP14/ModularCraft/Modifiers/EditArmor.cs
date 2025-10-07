using Content.Shared._CE14.ModularCraft;
using Content.Shared._CE14.ModularCraft.Components;
using Content.Shared.Armor;
using Content.Shared.Damage;

namespace Content.Server._CE14.ModularCraft.Modifiers;

public sealed partial class EditArmor : CE14ModularCraftModifier
{
    [DataField(required: true)]
    public DamageModifierSet Modifiers = new();

    public override void Effect(EntityManager entManager, Entity<CE14ModularCraftStartPointComponent> start, Entity<CE14ModularCraftPartComponent>? part)
    {
        if (!entManager.TryGetComponent<ArmorComponent>(start, out var armor))
            return;

        var armorSystem = entManager.System<SharedArmorSystem>();

        armorSystem.EditArmorCoefficients(start, Modifiers, armor);
    }
}
