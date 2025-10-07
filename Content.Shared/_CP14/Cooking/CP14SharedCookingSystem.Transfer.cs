/*
 * This file is sublicensed under MIT License
 * https://github.com/space-wizards/space-station-14/blob/master/LICENSE.TXT
 */

using Content.Shared._CE14.Cooking.Components;
using Content.Shared.Interaction;
using Robust.Shared.Containers;

namespace Content.Shared._CE14.Cooking;

public abstract partial class CE14SharedCookingSystem
{
    private void InitTransfer()
    {
        SubscribeLocalEvent<CE14FoodHolderComponent, AfterInteractEvent>(OnAfterInteract);
        SubscribeLocalEvent<CE14FoodHolderComponent, InteractUsingEvent>(OnInteractUsing);

        SubscribeLocalEvent<CE14FoodCookerComponent, ContainerIsInsertingAttemptEvent>(OnInsertAttempt);
    }

    private void OnInteractUsing(Entity<CE14FoodHolderComponent> target, ref InteractUsingEvent args)
    {
        if (!TryComp<CE14FoodHolderComponent>(args.Used, out var used))
            return;

        TryTransferFood(target, (args.Used, used));
    }

    private void OnAfterInteract(Entity<CE14FoodHolderComponent> ent, ref AfterInteractEvent args)
    {
        if (!TryComp<CE14FoodHolderComponent>(args.Target, out var target))
            return;

        TryTransferFood(ent, (args.Target.Value, target));
    }

    private void OnInsertAttempt(Entity<CE14FoodCookerComponent> ent, ref ContainerIsInsertingAttemptEvent args)
    {
        if (args.Cancelled)
            return;

        if (!TryComp<CE14FoodHolderComponent>(ent, out var holder))
            return;

        if (holder.FoodData is not null)
        {
            _popup.PopupEntity(Loc.GetString("CE14-cooking-popup-not-empty", ("name", MetaData(ent).EntityName)), ent);
            args.Cancel();
        }
    }
}
