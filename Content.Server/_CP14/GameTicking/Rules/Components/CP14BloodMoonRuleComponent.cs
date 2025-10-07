using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.GameTicking.Rules.Components;

[RegisterComponent, Access(typeof(CE14BloodMoonRule))]
public sealed partial class CE14BloodMoonRuleComponent : Component
{
    [DataField]
    public EntProtoId CurseRule = "CE14BloodMoonCurseRule";

    [DataField]
    public LocId StartAnnouncement = "CE14-bloodmoon-raising";

    [DataField]
    public Color? AnnouncementColor = Color.FromHex("#e32759");

    [DataField]
    public SoundSpecifier? AnnounceSound;
}
