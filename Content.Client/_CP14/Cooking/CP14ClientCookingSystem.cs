/*
 * This file is sublicensed under MIT License
 * https://github.com/space-wizards/space-station-14/blob/master/LICENSE.TXT
 */

using System.Numerics;
using Content.Client.DisplacementMap;
using Content.Shared._CE14.Cooking;
using Content.Shared._CE14.Cooking.Components;
using Content.Shared.DisplacementMap;
using Content.Shared.Rounding;
using Robust.Client.GameObjects;

namespace Content.Client._CE14.Cooking;

public sealed class CE14ClientCookingSystem : CE14SharedCookingSystem
{
    [Dependency] private readonly SpriteSystem _sprite = default!;
    [Dependency] private readonly DisplacementMapSystem _displacement = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14FoodHolderComponent, AfterAutoHandleStateEvent>(OnAfterHandleState);
        SubscribeLocalEvent<CE14FoodHolderComponent, AppearanceChangeEvent>(OnAppearanceChange);
    }

    private void OnAppearanceChange(Entity<CE14FoodHolderComponent> ent, ref AppearanceChangeEvent args)
    {
        UpdateVisuals(ent);
    }

    private void OnAfterHandleState(Entity<CE14FoodHolderComponent> ent, ref AfterAutoHandleStateEvent args)
    {
        UpdateVisuals(ent);
    }

    private void UpdateVisuals(Entity<CE14FoodHolderComponent> ent)
    {
        if (!TryComp<SpriteComponent>(ent, out var sprite))
            return;

        if (ent.Comp.FoodData is null)
            return;

        if (!_solution.TryGetSolution(ent.Owner, ent.Comp.SolutionId, out var soln, out var solution))
            return;

        //Remove old layers
        foreach (var key in ent.Comp.RevealedLayers)
        {
            _sprite.RemoveLayer((ent, sprite), key);
        }

        ent.Comp.RevealedLayers.Clear();


        _sprite.LayerMapTryGet((ent, sprite), ent.Comp.TargetLayerMap, out var index, false);

        var fillLevel = (float)solution.Volume / (float)solution.MaxVolume;
        if (fillLevel > 1)
            fillLevel = 1;

        var closestFillSprite = ContentHelpers.RoundToLevels(fillLevel, 1, ent.Comp.MaxDisplacementFillLevels + 1);

        //Add new layers
        var counter = 0;
        foreach (var layer in ent.Comp.FoodData.Visuals)
        {
            var layerIndex = index + counter;
            var keyCode = $"CE14-food-layer-{counter}";
            ent.Comp.RevealedLayers.Add(keyCode);

            _sprite.AddBlankLayer((ent, sprite), layerIndex);
            _sprite.LayerMapSet((ent, sprite), keyCode, layerIndex);
            _sprite.LayerSetData((ent, sprite), layerIndex, layer);

            if (ent.Comp.DisplacementRsiPath is not null)
            {
                var displaceData = new DisplacementData
                {
                    SizeMaps = new Dictionary<int, PrototypeLayerData>
                    {
                        {
                            32, new PrototypeLayerData
                            {
                                RsiPath = ent.Comp.DisplacementRsiPath,
                                State = "fill-" + closestFillSprite,
                            }
                        },
                    }
                };
                if (_displacement.TryAddDisplacement(displaceData,
                        (ent, sprite),
                        layerIndex,
                        keyCode,
                        out var displacementKey))
                {
                    ent.Comp.RevealedLayers.Add(displacementKey);
                }
            }

            counter++;
        }
    }
}
