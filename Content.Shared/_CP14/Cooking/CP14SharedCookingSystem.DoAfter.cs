/*
 * This file is sublicensed under MIT License
 * https://github.com/space-wizards/space-station-14/blob/master/LICENSE.TXT
 */

using Content.Shared._CE14.Cooking.Components;
using Content.Shared._CE14.Cooking.Prototypes;
using Content.Shared.DoAfter;
using Content.Shared.Temperature;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Timing;

namespace Content.Shared._CE14.Cooking;

public abstract partial class CE14SharedCookingSystem
{
    [Dependency] private readonly IGameTiming _timing = default!;
    private void InitDoAfter()
    {
        SubscribeLocalEvent<CE14FoodCookerComponent, OnTemperatureChangeEvent>(OnTemperatureChange);
        SubscribeLocalEvent<CE14FoodCookerComponent, EntParentChangedMessage>(OnParentChanged);

        SubscribeLocalEvent<CE14FoodCookerComponent, CE14CookingDoAfter>(OnCookFinished);
        SubscribeLocalEvent<CE14FoodCookerComponent, CE14BurningDoAfter>(OnCookBurned);
    }

    private void UpdateDoAfter(float frameTime)
    {
        var query = EntityQueryEnumerator<CE14FoodCookerComponent>();
        while(query.MoveNext(out var uid, out var cooker))
        {
            if (_timing.CurTime > cooker.LastHeatingTime + cooker.HeatingFrequencyRequired && _doAfter.IsRunning(cooker.DoAfterId))
                _doAfter.Cancel(cooker.DoAfterId);
        }
    }


    protected virtual void OnCookBurned(Entity<CE14FoodCookerComponent> ent, ref CE14BurningDoAfter args)
    {
        StopCooking(ent);

        if (args.Cancelled || args.Handled)
            return;

        BurntFood(ent);

        args.Handled = true;
    }

    protected virtual void OnCookFinished(Entity<CE14FoodCookerComponent> ent, ref CE14CookingDoAfter args)
    {
        StopCooking(ent);

        if (args.Cancelled || args.Handled)
            return;

        if (!TryComp<CE14FoodHolderComponent>(ent, out var holder))
            return;

        if (!_proto.TryIndex(args.Recipe, out var indexedRecipe))
            return;

        CreateFoodData(ent, indexedRecipe);
        UpdateFoodDataVisuals((ent, holder), ent.Comp.RenameCooker);

        args.Handled = true;
    }

    private void StartCooking(Entity<CE14FoodCookerComponent> ent, CE14CookingRecipePrototype recipe)
    {
        if (_doAfter.IsRunning(ent.Comp.DoAfterId))
            return;

        _appearance.SetData(ent, CE14CookingVisuals.Cooking, true);

        var doAfterArgs = new DoAfterArgs(EntityManager, ent, recipe.CookingTime, new CE14CookingDoAfter(recipe.ID), ent)
        {
            NeedHand = false,
            BreakOnWeightlessMove = false,
            RequireCanInteract = false,
        };

        _doAfter.TryStartDoAfter(doAfterArgs, out var doAfterId);
        ent.Comp.DoAfterId = doAfterId;
        _ambientSound.SetAmbience(ent, true);
    }

    private void StartBurning(Entity<CE14FoodCookerComponent> ent)
    {
        if (_doAfter.IsRunning(ent.Comp.DoAfterId))
            return;

        _appearance.SetData(ent, CE14CookingVisuals.Burning, true);

        var doAfterArgs = new DoAfterArgs(EntityManager, ent, 20, new CE14BurningDoAfter(), ent)
        {
            NeedHand = false,
            BreakOnWeightlessMove = false,
            RequireCanInteract = false,
        };

        _doAfter.TryStartDoAfter(doAfterArgs, out var doAfterId);
        ent.Comp.DoAfterId = doAfterId;
        _ambientSound.SetAmbience(ent, true);
    }

    protected void StopCooking(Entity<CE14FoodCookerComponent> ent)
    {
        if (_doAfter.IsRunning(ent.Comp.DoAfterId))
            _doAfter.Cancel(ent.Comp.DoAfterId);

        _appearance.SetData(ent, CE14CookingVisuals.Cooking, false);
        _appearance.SetData(ent, CE14CookingVisuals.Burning, false);

        _ambientSound.SetAmbience(ent, false);
    }

    private void OnTemperatureChange(Entity<CE14FoodCookerComponent> ent, ref OnTemperatureChangeEvent args)
    {
        if (!_container.TryGetContainer(ent, ent.Comp.ContainerId, out var container))
            return;

        if (!TryComp<CE14FoodHolderComponent>(ent, out var holder))
            return;

        if (container.ContainedEntities.Count <= 0 && holder.FoodData is null)
        {
            StopCooking(ent);
            return;
        }

        if (args.TemperatureDelta > 0)
        {
            ent.Comp.LastHeatingTime = _timing.CurTime;
            DirtyField(ent.Owner,ent.Comp, nameof(CE14FoodCookerComponent.LastHeatingTime));

            if (!_doAfter.IsRunning(ent.Comp.DoAfterId) && holder.FoodData is null)
            {
                var recipe = GetRecipe(ent);
                if (recipe is not null)
                    StartCooking(ent, recipe);
            }
            else
            {
                StartBurning(ent);
            }
        }
        else
        {
            StopCooking(ent);
        }
    }

    private void OnParentChanged(Entity<CE14FoodCookerComponent> ent, ref EntParentChangedMessage args)
    {
        StopCooking(ent);
    }
}

[Serializable, NetSerializable]
public sealed partial class CE14CookingDoAfter : DoAfterEvent
{
    [DataField]
    public ProtoId<CE14CookingRecipePrototype> Recipe;

    public CE14CookingDoAfter(ProtoId<CE14CookingRecipePrototype> recipe)
    {
        Recipe = recipe;
    }

    public override DoAfterEvent Clone() => this;
}

[Serializable, NetSerializable]
public sealed partial class CE14BurningDoAfter : SimpleDoAfterEvent;
