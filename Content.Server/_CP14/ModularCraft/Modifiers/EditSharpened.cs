using Content.Shared._CE14.MeleeWeapon.Components;
using Content.Shared._CE14.ModularCraft;
using Content.Shared._CE14.ModularCraft.Components;

namespace Content.Server._CE14.ModularCraft.Modifiers;

public sealed partial class EditSharpened : CE14ModularCraftModifier
{
    [DataField]
    public float SharpnessDamageMultiplier = 1f;
    public override void Effect(EntityManager entManager, Entity<CE14ModularCraftStartPointComponent> start, Entity<CE14ModularCraftPartComponent>? part)
    {
        if (!entManager.TryGetComponent<CE14SharpenedComponent>(start, out var sharpened))
            return;

        sharpened.SharpnessDamageBy1Damage *= SharpnessDamageMultiplier;
    }
}
