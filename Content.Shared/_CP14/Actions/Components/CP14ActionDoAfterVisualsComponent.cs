using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Actions.Components;

/// <summary>
/// Creates a temporary entity that exists while the spell is cast, and disappears at the end. For visual special effects.
/// </summary>
[RegisterComponent, Access(typeof(CE14SharedActionSystem))]
public sealed partial class CE14ActionDoAfterVisualsComponent : Component
{
    [DataField]
    public EntityUid? SpawnedEntity;

    [DataField(required: true)]
    public EntProtoId Proto = default!;
}
