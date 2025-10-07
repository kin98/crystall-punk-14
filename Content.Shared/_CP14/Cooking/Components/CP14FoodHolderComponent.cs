/*
 * This file is sublicensed under MIT License
 * https://github.com/space-wizards/space-station-14/blob/master/LICENSE.TXT
 */

using Content.Shared._CE14.Cooking.Prototypes;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Cooking.Components;

/// <summary>
/// Food of the specified type can be transferred to this entity.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(fieldDeltas: true, raiseAfterAutoHandleState: true), Access(typeof(CE14SharedCookingSystem))]
public sealed partial class CE14FoodHolderComponent : Component
{
    /// <summary>
    /// What food is currently stored here?
    /// </summary>
    [DataField, AutoNetworkedField]
    public CE14FoodData? FoodData;

    [DataField]
    public bool CanAcceptFood = false;

    [DataField]
    public bool CanGiveFood = false;

    [DataField(required: true)]
    public ProtoId<CE14FoodTypePrototype> FoodType;

    [DataField]
    public string? SolutionId;

    [DataField]
    public int MaxDisplacementFillLevels = 8;

    [DataField]
    public string? DisplacementRsiPath;

    /// <summary>
    /// target layer, where new layers will be added. This allows you to control the order of generative layers and static layers.
    /// </summary>
    [DataField]
    public string TargetLayerMap = "CE14_foodLayers";

    public HashSet<string> RevealedLayers = new();
}
