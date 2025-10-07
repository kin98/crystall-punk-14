using Content.Shared._CE14.MagicVision;
using Content.Shared.Eye;
using Robust.Shared.Timing;

namespace Content.Server._CE14.MagicVision;

public sealed class CE14MagicVisionSystem : CE14SharedMagicVisionSystem
{
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly SharedEyeSystem _eye = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MetaDataComponent, CE14MagicVisionToggleActionEvent>(OnMagicVisionToggle);
        SubscribeLocalEvent<CE14MagicVisionComponent, GetVisMaskEvent>(OnGetVisMask);
    }

    private void OnGetVisMask(Entity<CE14MagicVisionComponent> ent, ref GetVisMaskEvent args)
    {
        args.VisibilityMask |= (int)VisibilityFlags.CE14MagicVision;
    }

    private void OnMagicVisionToggle(Entity<MetaDataComponent> ent, ref CE14MagicVisionToggleActionEvent args)
    {
        if (!HasComp<CE14MagicVisionComponent>(ent))
        {
            AddComp<CE14MagicVisionComponent>(ent);
        }
        else
        {
            RemComp<CE14MagicVisionComponent>(ent);
        }
        _eye.RefreshVisibilityMask(ent.Owner);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<CE14MagicVisionMarkerComponent>();
        while (query.MoveNext(out var uid, out var marker))
        {
            if (marker.EndTime == TimeSpan.Zero)
                continue;

            if (_timing.CurTime < marker.EndTime)
                continue;

            QueueDel(uid);
        }
    }
}
