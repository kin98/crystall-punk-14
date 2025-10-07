using Content.Shared._CE14.MagicSpell.Events;
using Content.Shared._CE14.Vampire.Components;
using Content.Shared.Actions.Events;
using Content.Shared.Examine;
using Content.Shared.Mobs.Systems;
using Content.Shared.SSDIndicator;

namespace Content.Shared._CE14.Vampire;

public abstract partial class CE14SharedVampireSystem
{
    [Dependency] private readonly MobStateSystem _mobState = default!;

    private void InitializeSpell()
    {
        SubscribeLocalEvent<CE14MagicEffectVampireComponent, ActionAttemptEvent>(OnVampireCastAttempt);
        SubscribeLocalEvent<CE14MagicEffectVampireComponent, ExaminedEvent>(OnVampireCastExamine);
    }

    private void OnVampireCastAttempt(Entity<CE14MagicEffectVampireComponent> ent, ref ActionAttemptEvent args)
    {
        if (args.Cancelled)
            return;

        //If we are not vampires in principle, we certainly should not have this ability,
        //but then we will not limit its use to a valid vampire form that is unavailable to us.

        if (!HasComp<CE14VampireComponent>(args.User))
            return;

        if (!HasComp<CE14VampireVisualsComponent>(args.User))
        {
            _popup.PopupClient(Loc.GetString("CE14-magic-spell-need-vampire-valid"), args.User, args.User);
            args.Cancelled = true;
        }
    }

    private void OnVampireCastExamine(Entity<CE14MagicEffectVampireComponent> ent, ref ExaminedEvent args)
    {
        args.PushMarkup($"{Loc.GetString("CE14-magic-spell-need-vampire-valid")}");
    }
}
