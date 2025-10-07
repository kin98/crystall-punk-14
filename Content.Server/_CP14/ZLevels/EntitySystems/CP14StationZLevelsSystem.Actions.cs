using Content.Shared._CE14.ZLevel;
using Content.Shared.Actions;
using Robust.Shared.Map;

namespace Content.Server._CE14.ZLevels.EntitySystems;

public sealed partial class CE14StationZLevelsSystem
{
    [Dependency] private readonly SharedActionsSystem _actions = default!;
    private void InitActions()
    {
        SubscribeLocalEvent<CE14ZLevelMoverComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<CE14ZLevelMoverComponent, ComponentRemove>(OnRemove);
        SubscribeLocalEvent<CE14ZLevelMoverComponent, CE14ZLevelActionUp>(OnZLevelUpGhost);
        SubscribeLocalEvent<CE14ZLevelMoverComponent, CE14ZLevelActionDown>(OnZLevelDownGhost);
    }

    private void OnMapInit(Entity<CE14ZLevelMoverComponent> ent, ref MapInitEvent args)
    {
        _actions.AddAction(ent, ref ent.Comp.CE14ZLevelUpActionEntity, ent.Comp.UpActionProto);
        _actions.AddAction(ent, ref ent.Comp.CE14ZLevelDownActionEntity, ent.Comp.DownActionProto);
    }

    private void OnRemove(Entity<CE14ZLevelMoverComponent> ent, ref ComponentRemove args)
    {
        _actions.RemoveAction(ent.Comp.CE14ZLevelUpActionEntity);
        _actions.RemoveAction(ent.Comp.CE14ZLevelDownActionEntity);
    }

    private void OnZLevelDownGhost(Entity<CE14ZLevelMoverComponent> ent, ref CE14ZLevelActionDown args)
    {
        if (args.Handled)
            return;

        ZLevelMove(ent, -1);

        args.Handled = true;
    }

    private void OnZLevelUpGhost(Entity<CE14ZLevelMoverComponent> ent, ref CE14ZLevelActionUp args)
    {
        if (args.Handled)
            return;

        ZLevelMove(ent, 1);

        args.Handled = true;
    }

    private void ZLevelMove(EntityUid ent, int offset)
    {
        var xform = Transform(ent);
        var map = xform.MapUid;

        if (map is null)
            return;

        var targetMap = GetMapOffset(map.Value, offset);

        if (targetMap is null)
            return;

        _transform.SetMapCoordinates(ent, new MapCoordinates(_transform.GetWorldPosition(ent), targetMap.Value));
    }
}
