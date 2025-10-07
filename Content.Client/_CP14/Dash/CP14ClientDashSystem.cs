using Content.Shared._CE14.Dash;

namespace Content.Client._CE14.Dash;

public sealed partial class CE14ClientDashSystem : EntitySystem
{
    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<CE14DashComponent>();
        while (query.MoveNext(out var uid, out var dash))
        {
            SpawnAtPosition(dash.DashEffect, Transform(uid).Coordinates);
        }
    }
}
