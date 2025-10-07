using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.BloodMoon;


[RegisterComponent, NetworkedComponent]
public sealed partial class CE14BloodMoonCurseComponent : Component
{
    [DataField]
    public EntityUid? CurseRule;

    [DataField]
    public EntProtoId CurseEffect = "CE14BloodMoonCurseEffect";

    [DataField]
    public EntityUid? SpawnedEffect;

    [DataField]
    public TimeSpan EndStunDuration = TimeSpan.FromSeconds(60f);

    [DataField]
    public EntProtoId Action = "CE14ActionSpellBloodlust";

    [DataField]
    public EntityUid? ActionEntity;
}
