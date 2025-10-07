using Content.Shared.Actions;

namespace Content.Shared._CE14.NightVision;

public abstract class CE14SharedNightVisionSystem : EntitySystem
{
    [Dependency] private readonly SharedActionsSystem _actions = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14NightVisionComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<CE14NightVisionComponent, ComponentRemove>(OnRemove);
    }

    private void OnMapInit(Entity<CE14NightVisionComponent> ent, ref MapInitEvent args)
    {
        _actions.AddAction(ent, ref ent.Comp.ActionEntity, ent.Comp.ActionPrototype);
    }

    protected virtual void OnRemove(Entity<CE14NightVisionComponent> ent, ref ComponentRemove args)
    {
        _actions.RemoveAction(ent.Owner, ent.Comp.ActionEntity);
    }
}

public sealed partial class CE14ToggleNightVisionEvent : InstantActionEvent { }
