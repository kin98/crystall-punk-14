using Content.Server._CE14.Objectives.Systems;
using Content.Shared._CE14.Vampire;
using Content.Shared.StatusIcon;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Server._CE14.Objectives.Components;

[RegisterComponent, Access(typeof(CE14VampireObjectiveConditionsSystem))]
public sealed partial class CE14VampireBloodPurityConditionComponent : Component
{
    [DataField]
    public ProtoId<CE14VampireFactionPrototype>? Faction;

    [DataField]
    public SpriteSpecifier Icon = new SpriteSpecifier.Rsi(new ResPath("/Textures/_CE14/Actions/Spells/vampire.rsi"), "blood_moon");
}
