using System.Text;
using Content.Client.Items;
using Content.Client.Stylesheets;
using Content.Shared._CE14.LockKey.Components;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Timing;

namespace Content.Client._CE14.LockKey;

public sealed class CE14ClientLockKeySystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        Subs.ItemStatus<CE14KeyComponent>(ent => new CE14KeyStatusControl(ent));
    }
}


public sealed class CE14KeyStatusControl : Control
{
    private readonly Entity<CE14KeyComponent> _parent;
    private readonly RichTextLabel _label;
    public CE14KeyStatusControl(Entity<CE14KeyComponent> parent)
    {
        _parent = parent;

        _label = new RichTextLabel { StyleClasses = { StyleNano.StyleClassItemStatus } };
        AddChild(_label);
    }

    protected override void FrameUpdate(FrameEventArgs args)
    {
        base.FrameUpdate(args);

        if (_parent.Comp.LockShape is null)
            return;

        var sb = new StringBuilder("(");
        foreach (var item in _parent.Comp.LockShape)
        {
            sb.Append($"{item} ");
        }

        sb.Append(")");
        _label.Text = sb.ToString();
    }
}
