using Content.Shared._CE14.ModularCraft.Components;
using Content.Shared.DoAfter;
using Content.Shared.Examine;
using Content.Shared.Interaction;
using Content.Shared.Labels.EntitySystems;
using Robust.Shared.Serialization;

namespace Content.Shared._CE14.ModularCraft;

public abstract class CE14SharedModularCraftSystem : EntitySystem
{
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;
    [Dependency] private readonly LabelSystem _label = default!;
    [Dependency] private readonly MetaDataSystem _meta = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14ModularCraftStartPointComponent, AfterInteractEvent>(OnAfterInteractStart);
        SubscribeLocalEvent<CE14ModularCraftPartComponent, AfterInteractEvent>(OnAfterInteractPart);
        SubscribeLocalEvent<CE14LabeledRenamingComponent, CE14LabeledEvent>(OnLabelRenaming);
    }

    private void OnLabelRenaming(Entity<CE14LabeledRenamingComponent> ent, ref CE14LabeledEvent args)
    {
        if (args.Text is null)
            return;
        _meta.SetEntityName(ent, args.Text);
        _label.Label(ent, null);
    }

    private void OnAfterInteractStart(Entity<CE14ModularCraftStartPointComponent> start, ref AfterInteractEvent args)
    {
        if (args.Handled || args.Target is null)
            return;

        if (!TryComp<CE14ModularCraftPartComponent>(args.Target, out var part))
            return;

        var xform = Transform(args.Target.Value);
        if (xform.GridUid != xform.ParentUid)
            return;

        _doAfter.TryStartDoAfter(new DoAfterArgs(EntityManager,
            args.User,
            part.DoAfter,
            new CE14ModularCraftAddPartDoAfter(),
            args.Target,
            args.Target,
            start)
        {
            BreakOnDamage = true,
            BreakOnMove = true,
            BreakOnDropItem = true,
        });

        args.Handled = true;
    }

    private void OnAfterInteractPart(Entity<CE14ModularCraftPartComponent> part, ref AfterInteractEvent args)
    {
        if (args.Handled || args.Target is null)
            return;

        if (!HasComp<CE14ModularCraftStartPointComponent>(args.Target))
            return;

        var xform = Transform(args.Target.Value);
        if (xform.GridUid != xform.ParentUid)
            return;

        _doAfter.TryStartDoAfter(new DoAfterArgs(EntityManager,
            args.User,
            part.Comp.DoAfter,
            new CE14ModularCraftAddPartDoAfter(),
            args.Target,
            args.Target,
            part)
        {
            BreakOnDamage = true,
            BreakOnMove = true,
            BreakOnDropItem = true,
        });

        args.Handled = true;
    }
}

[Serializable, NetSerializable]
public sealed partial class CE14ModularCraftAddPartDoAfter : SimpleDoAfterEvent
{
}
