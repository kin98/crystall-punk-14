using Content.Shared._CE14.Religion.Components;
using Content.Shared._CE14.Religion.Systems;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellGodRenounce : CE14SpellEffect
{
    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.Target is null)
            return;

        if (!entManager.TryGetComponent<CE14ReligionEntityComponent>(args.User, out var god) || god.Religion is null)
            return;

        if (!entManager.TryGetComponent<CE14ReligionFollowerComponent>(args.Target.Value, out var follower) || follower.Religion != god.Religion)
            return;

        var religionSys = entManager.System<CE14SharedReligionGodSystem>();

        religionSys.ToDisbelieve(args.Target.Value);
    }
}
