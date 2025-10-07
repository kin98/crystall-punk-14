using Content.Shared.Stealth;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellRevealStealthUser : CE14SpellEffect
{
    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.User is null)
            return;

        var stealth = entManager.System<SharedStealthSystem>();

        stealth.SetVisibility(args.User.Value, 1);
    }
}
