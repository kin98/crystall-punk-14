using Content.Shared._CE14.CloudShadow;
using Robust.Shared.Random;

namespace Content.Server._CE14.CloudShadow;

public sealed class CE14CloudShadowsSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14CloudShadowsComponent, MapInitEvent>(OnMapInit);
    }

    private void OnMapInit(Entity<CE14CloudShadowsComponent> entity, ref MapInitEvent args)
    {
        entity.Comp.CloudSpeed = _random.NextVector2(-entity.Comp.MaxSpeed, entity.Comp.MaxSpeed);
    }
}
