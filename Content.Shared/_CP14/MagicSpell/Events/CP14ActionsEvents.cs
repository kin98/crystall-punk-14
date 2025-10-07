using Content.Shared.Actions;

namespace Content.Shared._CE14.MagicSpell.Events;

public interface ICE14MagicEffect
{
    public TimeSpan Cooldown { get; }
}

public sealed partial class CE14WorldTargetActionEvent : WorldTargetActionEvent, ICE14MagicEffect
{
    [DataField]
    public TimeSpan Cooldown { get; private set; } = TimeSpan.FromSeconds(1f);
}

public sealed partial class CE14EntityTargetActionEvent : EntityTargetActionEvent, ICE14MagicEffect
{
    [DataField]
    public TimeSpan Cooldown { get; private set; } = TimeSpan.FromSeconds(1f);
}

public sealed partial class CE14InstantActionEvent : InstantActionEvent, ICE14MagicEffect
{
    [DataField]
    public TimeSpan Cooldown { get; private set; } = TimeSpan.FromSeconds(1f);
}

