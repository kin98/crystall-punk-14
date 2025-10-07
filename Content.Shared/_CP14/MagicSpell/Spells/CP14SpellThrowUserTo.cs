using Content.Shared.Throwing;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellThrowUserTo : CE14SpellEffect
{
    [DataField]
    public float ThrowPower = 10f;

    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.Position is null || args.User is null)
            return;

        var throwing = entManager.System<ThrowingSystem>();

        throwing.TryThrow(args.User.Value, args.Position.Value, ThrowPower);
    }
}
