
using Content.Shared._CE14.MagicRitual.Prototypes;
using Content.Shared._CE14.MagicSpell.Events;
using Content.Shared.Examine;
using Content.Shared.FixedPoint;
using Content.Shared.Inventory;
using Content.Shared.Verbs;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._CE14.MagicManacostModify;

public sealed partial class CE14MagicManacostModifySystem : EntitySystem
{
    [Dependency] private readonly ExamineSystemShared _examine = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14MagicManacostModifyComponent, InventoryRelayedEvent<CE14CalculateManacostEvent>>(OnCalculateManacost);
        SubscribeLocalEvent<CE14MagicManacostModifyComponent, CE14CalculateManacostEvent>(OnCalculateManacost);
        SubscribeLocalEvent<CE14MagicManacostModifyComponent, GetVerbsEvent<ExamineVerb>>(OnVerbExamine);
    }

    private void OnVerbExamine(Entity<CE14MagicManacostModifyComponent> ent, ref GetVerbsEvent<ExamineVerb> args)
    {
        if (!args.CanInteract || !args.CanAccess || !ent.Comp.Examinable)
            return;

        var markup = GetManacostModifyMessage(ent.Comp.GlobalModifier);
        _examine.AddDetailedExamineVerb(
            args,
            ent.Comp,
            markup,
            Loc.GetString("CE14-magic-examinable-verb-text"),
            "/Textures/Interface/VerbIcons/bubbles.svg.192dpi.png",
            Loc.GetString("CE14-magic-examinable-verb-message"));
    }

    public FormattedMessage GetManacostModifyMessage(FixedPoint2 global)
    {
        var msg = new FormattedMessage();
        msg.AddMarkupOrThrow(Loc.GetString("CE14-clothing-magic-examine"));

        if (global != 1)
        {
            msg.PushNewline();

            var plus = (float)global > 1 ? "+" : "";
            msg.AddMarkupOrThrow(
                $"{Loc.GetString("CE14-clothing-magic-global")}: {plus}{MathF.Round((float)(global - 1) * 100, MidpointRounding.AwayFromZero)}%");
        }

        return msg;
    }

    private void OnCalculateManacost(Entity<CE14MagicManacostModifyComponent> ent, ref InventoryRelayedEvent<CE14CalculateManacostEvent> args)
    {
        OnCalculateManacost(ent, ref args.Args);
    }

    private void OnCalculateManacost(Entity<CE14MagicManacostModifyComponent> ent, ref CE14CalculateManacostEvent args)
    {
        args.Multiplier *= (float)ent.Comp.GlobalModifier;
    }
}
