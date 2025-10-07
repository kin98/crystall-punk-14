using Robust.Shared.Audio;

namespace Content.Shared._CE14.FarSound;

[RegisterComponent]
public sealed partial class CE14FarSoundComponent : Component
{
    [DataField]
    public SoundSpecifier? CloseSound;

    [DataField]
    public SoundSpecifier? FarSound;

    [DataField]
    public float FarRange = 50f;
}
