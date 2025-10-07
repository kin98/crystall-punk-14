using Content.Shared.FixedPoint;
using Robust.Shared.GameStates;

namespace Content.Shared._CE14.Vampire.Components;

[RegisterComponent]
[NetworkedComponent]
[AutoGenerateComponentState]
[Access(typeof(CE14SharedVampireSystem))]
public sealed partial class CE14VampireEssenceHolderComponent : Component
{
    [DataField, AutoNetworkedField]
    public FixedPoint2 Essence = 1f;
}
