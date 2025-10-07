using Content.Shared.EntityEffects;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellApplyEntityEffectOnUser : CE14SpellEffect
{
    [DataField(required: true, serverOnly: true)]
    public List<EntityEffect> Effects = new();

    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.User == null)
            return;

        foreach (var effect in Effects)
        {
            effect.Effect(new EntityEffectBaseArgs(args.User.Value, entManager));
        }
    }
}
