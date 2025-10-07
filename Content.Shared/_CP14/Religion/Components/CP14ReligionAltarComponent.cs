using Content.Shared._CE14.Religion.Prototypes;
using Content.Shared._CE14.Religion.Systems;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Religion.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState, Access(typeof(CE14SharedReligionGodSystem))]
public sealed partial class CE14ReligionAltarComponent : Component
{
    [DataField, AutoNetworkedField]
    public ProtoId<CE14ReligionPrototype>? Religion;

    [DataField, AutoNetworkedField]
    public bool CanBeConverted = true;
}
