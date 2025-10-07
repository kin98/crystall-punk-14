using Robust.Shared.Audio;

namespace Content.Server._CE14.PersonalSignature;

[RegisterComponent]
public sealed partial class CE14PersonalSignatureComponent : Component
{
    [DataField]
    public SoundSpecifier? SignSound;
}
