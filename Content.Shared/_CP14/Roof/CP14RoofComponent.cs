using Content.Shared.Actions;

namespace Content.Shared._CE14.Roof;

/// <summary>
/// Marks an entity as a roof, allowing you to hide all roofs or show them back depending on different situations
/// </summary>
[RegisterComponent]
public sealed partial class CE14RoofComponent : Component
{
}

public sealed partial class CE14ToggleRoofVisibilityAction : InstantActionEvent
{
}
