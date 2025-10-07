using Content.Shared._CE14.Actions.Components;
using Content.Shared.Actions.Events;
using Content.Shared.Movement.Systems;

namespace Content.Shared._CE14.Actions;

public abstract partial class CE14SharedActionSystem
{
    private void InitializeDoAfter()
    {
        SubscribeLocalEvent<CE14ActionDoAfterSlowdownComponent, CE14ActionStartDoAfterEvent>(OnStartDoAfter);
        SubscribeLocalEvent<CE14ActionDoAfterSlowdownComponent, ActionDoAfterEvent>(OnEndDoAfter);
        SubscribeLocalEvent<CE14SlowdownFromActionsComponent, RefreshMovementSpeedModifiersEvent>(OnRefreshMovespeed);
    }

    private void OnStartDoAfter(Entity<CE14ActionDoAfterSlowdownComponent> ent, ref CE14ActionStartDoAfterEvent args)
    {
        var performer = GetEntity(args.Performer);
        EnsureComp<CE14SlowdownFromActionsComponent>(performer, out var slowdown);

        slowdown.SpeedAffectors.TryAdd(GetNetEntity(ent), ent.Comp.SpeedMultiplier);
        Dirty(performer, slowdown);
        _movement.RefreshMovementSpeedModifiers(performer);
    }

    private void OnEndDoAfter(Entity<CE14ActionDoAfterSlowdownComponent> ent, ref ActionDoAfterEvent args)
    {
        if (args.Repeat)
            return;

        var performer = GetEntity(args.Performer);
        if (!TryComp<CE14SlowdownFromActionsComponent>(performer, out var slowdown))
            return;

        slowdown.SpeedAffectors.Remove(GetNetEntity(ent));
        Dirty(performer, slowdown);

        _movement.RefreshMovementSpeedModifiers(performer);

        if (slowdown.SpeedAffectors.Count == 0)
            RemCompDeferred<CE14SlowdownFromActionsComponent>(performer);
    }

    private void OnRefreshMovespeed(Entity<CE14SlowdownFromActionsComponent> ent, ref RefreshMovementSpeedModifiersEvent args)
    {
        var targetSpeedModifier = 1f;

        foreach (var (_, affector) in ent.Comp.SpeedAffectors)
        {
            targetSpeedModifier *= affector;
        }

        args.ModifySpeed(targetSpeedModifier);
    }
}
