using Content.Shared._CE14.Religion.Components;
using Content.Shared._CE14.Religion.Systems;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellSendMessageToGod : CE14SpellEffect
{
    [DataField]
    public LocId? Message;

    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (!entManager.TryGetComponent<CE14ReligionFollowerComponent>(args.User, out var follower))
            return;
        if (!entManager.TryGetComponent<MetaDataComponent>(args.User, out var metaData))
            return;

        if (follower.Religion is null)
            return;

        var religionSys = entManager.System<CE14SharedReligionGodSystem>();

        religionSys.SendMessageToGods(follower.Religion.Value, Loc.GetString("CE14-call-follower-message", ("name", metaData.EntityName)) + " " + Loc.GetString(Message?? ""), args.User.Value);
    }
}
