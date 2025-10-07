using Content.Shared._CE14.MagicEnergy;
using Content.Shared._CE14.Religion.Components;
using Content.Shared._CE14.Religion.Systems;
using Content.Shared.FixedPoint;

namespace Content.Shared._CE14.MagicSpell.Spells;

public sealed partial class CE14SpellTransferManaToGod : CE14SpellEffect
{
    [DataField]
    public FixedPoint2 Amount = 10f;

    [DataField]
    public bool Safe = false;

    public override void Effect(EntityManager entManager, CE14SpellEffectBaseArgs args)
    {
        if (args.User is null)
            return;

        if (!entManager.TryGetComponent<CE14ReligionFollowerComponent>(args.User, out var follower))
            return;

        if (follower.Religion is null)
            return;

        var religionSys = entManager.System<CE14SharedReligionGodSystem>();
        var magicEnergySys = entManager.System<CE14SharedMagicEnergySystem>();

        var gods = religionSys.GetGods(follower.Religion.Value);
        var manaAmount = Amount / gods.Count;
        foreach (var god in gods)
        {
            magicEnergySys.TransferEnergy(args.User.Value, god.Owner, manaAmount, out _, out _, safe: Safe);
        }
    }
}
