using Content.Shared._CE14.Religion.Components;
using Content.Shared._CE14.Religion.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellGodTouch : CE14SpellEffect
{
    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.Target is null)
            return;

        if (!entManager.TryGetComponent<CE14ReligionEntityComponent>(args.User, out var god) || god.Religion is null)
            return;

        var ev = new CE14GodTouchEvent(god.Religion.Value);
        entManager.EventBus.RaiseLocalEvent(args.Target.Value, ev);
    }
}

public sealed class CE14GodTouchEvent(ProtoId<CE14ReligionPrototype> religion) : EntityEventArgs
{
    public ProtoId<CE14ReligionPrototype> Religion = religion;
}
