using Content.Shared._CE14.Vampire;
using Content.Shared._CE14.Vampire.Components;
using Content.Shared.FixedPoint;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellVampireGatherEssence : CE14SpellEffect
{
    [DataField]
    public FixedPoint2 Amount = 0.2f;

    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.Target is null)
            return;

        if (args.User is null)
            return;

        if (entManager.HasComponent<CE14VampireComponent>(args.Target.Value))
            return;

        if (!entManager.TryGetComponent<CE14VampireEssenceHolderComponent>(args.Target.Value, out var essenceHolder))
            return;

        var vamp = entManager.System<CE14SharedVampireSystem>();
        vamp.GatherEssence(args.User.Value, args.Target.Value, Amount);
    }
}
