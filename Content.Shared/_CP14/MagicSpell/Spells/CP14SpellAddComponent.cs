using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellAddComponent : CE14SpellEffect
{
    [DataField]
    public ComponentRegistry Components = new();

    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.Target is null)
            return;

        entManager.AddComponents(args.Target.Value, Components);
    }
}
