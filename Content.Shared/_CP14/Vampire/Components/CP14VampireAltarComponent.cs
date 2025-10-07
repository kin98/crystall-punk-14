using Robust.Shared.GameStates;

namespace Content.Shared._CE14.Vampire.Components;

/// <summary>
/// increases the amount of blood essence extracted if the victim is strapped to the altar
/// </summary>
[RegisterComponent]
[NetworkedComponent]
[AutoGenerateComponentState]
[Access(typeof(CE14SharedVampireSystem))]
public sealed partial class CE14VampireAltarComponent : Component
{
    [DataField, AutoNetworkedField]
    public float Multiplier = 2f;
}
