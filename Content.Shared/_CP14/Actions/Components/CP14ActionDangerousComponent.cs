using Content.Shared._CE14.MagicSpell;
using Robust.Shared.GameStates;

namespace Content.Shared._CE14.Actions.Components;

/// <summary>
/// Blocks the target from using magic if they are pacified.
/// Also block using spell on SSD player
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class CE14ActionDangerousComponent : Component
{
}
