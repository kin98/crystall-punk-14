using Content.Server._CE14.MagicSpellStorage.Components;
using Content.Shared.Clothing;
using Content.Shared.Hands;

namespace Content.Server._CE14.MagicSpellStorage;

public sealed partial class CE14SpellStorageSystem
{
    private void InitializeAccess()
    {
        SubscribeLocalEvent<CE14SpellStorageAccessHoldingComponent, GotEquippedHandEvent>(OnEquippedHand);

        SubscribeLocalEvent<CE14SpellStorageAccessWearingComponent, ClothingGotEquippedEvent>(OnClothingEquipped);
        SubscribeLocalEvent<CE14SpellStorageAccessWearingComponent, ClothingGotUnequippedEvent>(OnClothingUnequipped);
    }

    private void OnEquippedHand(Entity<CE14SpellStorageAccessHoldingComponent> ent, ref GotEquippedHandEvent args)
    {
        if (!TryComp<CE14SpellStorageComponent>(ent, out var spellStorage))
            return;

        TryGrantAccess((ent, spellStorage), args.User);
    }

    private void OnClothingEquipped(Entity<CE14SpellStorageAccessWearingComponent> ent, ref ClothingGotEquippedEvent args)
    {
        ent.Comp.Wearing = true;

        if (!TryComp<CE14SpellStorageComponent>(ent, out var spellStorage))
            return;

        TryGrantAccess((ent, spellStorage), args.Wearer);
    }

    private void OnClothingUnequipped(Entity<CE14SpellStorageAccessWearingComponent> ent, ref ClothingGotUnequippedEvent args)
    {
        ent.Comp.Wearing = false;

        _actions.RemoveProvidedActions(args.Wearer, ent);
    }
}
