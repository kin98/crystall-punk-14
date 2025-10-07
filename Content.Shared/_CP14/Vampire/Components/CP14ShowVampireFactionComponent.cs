using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Vampire.Components;

[RegisterComponent]
[NetworkedComponent]
[AutoGenerateComponentState]
public sealed partial class CE14ShowVampireFactionComponent : Component
{
    [DataField, AutoNetworkedField]
    public ProtoId<CE14VampireFactionPrototype>? Faction;
}
