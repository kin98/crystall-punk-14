using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Shared._CE14.MagicEssence;

/// <summary>
///
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class CE14MagicEssenceComponent : Component
{
    [DataField]
    public string Solution = "essence";

    [DataField]
    public SoundSpecifier ConsumeSound = new SoundPathSpecifier("/Audio/_CE14/Effects/essence_consume.ogg")
    {
        Params = AudioParams.Default.WithVolume(-2f).WithVariation(0.2f),
    };
}
