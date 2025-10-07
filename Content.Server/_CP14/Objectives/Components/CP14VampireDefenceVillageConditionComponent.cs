using Content.Server._CE14.Objectives.Systems;
using Robust.Shared.Utility;

namespace Content.Server._CE14.Objectives.Components;

[RegisterComponent, Access(typeof(CE14VampireObjectiveConditionsSystem))]
public sealed partial class CE14VampireDefenceVillageConditionComponent : Component
{
    [DataField]
    public SpriteSpecifier Icon = new SpriteSpecifier.Rsi(new ResPath("/Textures/_CE14/Actions/Spells/vampire.rsi"), "essence_create");
}
