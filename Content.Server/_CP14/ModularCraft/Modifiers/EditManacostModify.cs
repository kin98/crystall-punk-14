using Content.Shared._CE14.ModularCraft;
using Content.Shared._CE14.ModularCraft.Components;
using Content.Shared._CE14.MagicManacostModify;
using Content.Shared._CE14.MagicRitual.Prototypes;
using Content.Shared.FixedPoint;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.ModularCraft.Modifiers;

public sealed partial class EditManacostModify : CE14ModularCraftModifier
{
    [DataField]
    public FixedPoint2 GlobalModifier = 1f;

    public override void Effect(EntityManager entManager, Entity<CE14ModularCraftStartPointComponent> start, Entity<CE14ModularCraftPartComponent>? part)
    {
        if (!entManager.TryGetComponent<CE14MagicManacostModifyComponent>(start, out var manacostModifyComp))
            return;

        manacostModifyComp.GlobalModifier += GlobalModifier;
    }
}
