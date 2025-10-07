using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._CE14.Skill.Prototypes;

/// <summary>
/// A group of skills combined into one “branch”
/// </summary>
[Prototype("CE14SkillPoint")]
public sealed partial class CE14SkillPointPrototype : IPrototype
{
    [IdDataField] public string ID { get; } = default!;

    [DataField(required: true)]
    public LocId Name;

    [DataField]
    public SpriteSpecifier? Icon;

    [DataField]
    public LocId? GetPointPopup;

    [DataField]
    public LocId? LosePointPopup;
}
