using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Dash;

/// <summary>
/// This component marks entities that are currently in the dash
/// </summary>
[RegisterComponent, NetworkedComponent, Access(typeof(CE14DashSystem))]
public sealed partial class CE14DashComponent : Component
{
    [DataField]
    public EntProtoId DashEffect = "CE14DustEffect";

    [DataField]
    public SoundSpecifier DashSound = new SoundPathSpecifier("/Audio/_CE14/Effects/dash.ogg")
    {
        Params = AudioParams.Default.WithVariation(0.05f)
    };
}
