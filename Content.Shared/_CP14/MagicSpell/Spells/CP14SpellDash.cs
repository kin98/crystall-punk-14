using Content.Shared._CE14.Dash;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellDash : CE14SpellEffect
{
    [DataField]
    public float Speed = 10f;

    [DataField]
    public float Range = 3.5f;

    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.User is null)
            return;
        if (args.Position is null)
            return;

        var dashSys = entManager.System<CE14DashSystem>();

        dashSys.PerformDash(args.User.Value, args.Position.Value, Speed, Range);
    }
}
