using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.GameTicking.Rules.Components;

[RegisterComponent, Access(typeof(CE14BloodMoonCurseRule))]
public sealed partial class CE14BloodMoonCurseRuleComponent : Component
{
    [DataField]
    public LocId StartAnnouncement = "CE14-bloodmoon-start";

    [DataField]
    public LocId EndAnnouncement = "CE14-bloodmoon-end";

    [DataField]
    public Color? AnnouncementColor;

    [DataField]
    public EntProtoId CurseEffect = "CE14ImpactEffectMagicSplitting";

    [DataField]
    public SoundSpecifier GlobalSound = new SoundPathSpecifier("/Audio/_CE14/Ambience/blood_moon_raise.ogg")
    {
        Params = AudioParams.Default.WithVolume(-2f)
    };
}
