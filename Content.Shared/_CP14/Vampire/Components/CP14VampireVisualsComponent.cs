using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Vampire;

[RegisterComponent, NetworkedComponent]
public sealed partial class CE14VampireVisualsComponent : Component
{
    [DataField]
    public Color EyesColor = Color.Red;

    [DataField]
    public Color OriginalEyesColor = Color.White;

    [DataField]
    public string FangsMap = "vampire_fangs";

    [DataField]
    public EntProtoId EnableVFX = "CE14ImpactEffectBloodEssence2";

    [DataField]
    public EntProtoId DisableVFX = "CE14ImpactEffectBloodEssenceInverse";
}
