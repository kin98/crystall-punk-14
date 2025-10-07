using Content.Shared.FixedPoint;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Shared._CE14.Vampire.Components;

[RegisterComponent]
[NetworkedComponent]
[Access(typeof(CE14SharedVampireSystem))]
public sealed partial class CE14VampireTreeCollectableComponent : Component
{
    [DataField]
    public FixedPoint2 Essence = 1f;

    [DataField]
    public SoundSpecifier CollectSound = new SoundPathSpecifier("/Audio/_CE14/Effects/essence_consume.ogg");
}
