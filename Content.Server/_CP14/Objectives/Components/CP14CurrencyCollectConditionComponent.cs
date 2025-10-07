using Content.Server._CE14.Objectives.Systems;
using Robust.Shared.Utility;

namespace Content.Server._CE14.Objectives.Components;

[RegisterComponent, Access(typeof(CE14CurrencyCollectConditionSystem))]
public sealed partial class CE14CurrencyCollectConditionComponent : Component
{
    [DataField]
    public int Currency = 1000;

    [DataField(required: true)]
    public LocId ObjectiveText;

    [DataField(required: true)]
    public LocId ObjectiveDescription;

    [DataField(required: true)]
    public SpriteSpecifier ObjectiveSprite;
}
