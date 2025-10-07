using Content.Shared._CE14.Religion.Prototypes;
using Content.Shared._CE14.Religion.Systems;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Religion.Components;

/// <summary>
/// Allows the god of a particular religion to see within a radius around the observer.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(true), Access(typeof(CE14SharedReligionGodSystem))]
public sealed partial class CE14ReligionObserverComponent : Component
{
    [DataField, AutoNetworkedField]
    public ProtoId<CE14ReligionPrototype>? Religion;

    [DataField, AutoNetworkedField]
    public float Radius = 5f;

    [DataField, AutoNetworkedField]
    public bool Active = true;
}
