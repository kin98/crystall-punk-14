
using Content.Shared._CE14.LockKey;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.LockKey;

[RegisterComponent]
public sealed partial class CE14AbstractKeyComponent : Component
{
    [DataField(required: true)]
    public ProtoId<CE14LockGroupPrototype> Group = default;

    [DataField]
    public bool DeleteOnFailure = true;
}
