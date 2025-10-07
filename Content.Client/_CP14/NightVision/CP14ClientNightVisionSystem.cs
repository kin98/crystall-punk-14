using Content.Shared._CE14.NightVision;
using Robust.Client.Player;
using Robust.Shared.Player;

namespace Content.Client._CE14.NightVision;

public sealed class CE14ClientNightVisionSystem : CE14SharedNightVisionSystem
{
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    [Dependency] private readonly IPlayerManager _playerManager = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14NightVisionComponent, CE14ToggleNightVisionEvent>(OnToggleNightVision);
        SubscribeLocalEvent<CE14NightVisionComponent, PlayerDetachedEvent>(OnPlayerDetached);
    }

    protected override void OnRemove(Entity<CE14NightVisionComponent> ent, ref ComponentRemove args)
    {
        base.OnRemove(ent, ref args);

        NightVisionOff(ent);
    }

    private void OnPlayerDetached(Entity<CE14NightVisionComponent> ent, ref PlayerDetachedEvent args)
    {
        NightVisionOff(ent);
    }

    private void OnToggleNightVision(Entity<CE14NightVisionComponent> ent, ref CE14ToggleNightVisionEvent args)
    {
        NightVisionToggle(ent);
    }

    private void NightVisionOn(Entity<CE14NightVisionComponent> ent)
    {
        if (_playerManager.LocalSession?.AttachedEntity != ent)
            return;

        var nightVisionLight = Spawn(ent.Comp.LightPrototype, Transform(ent).Coordinates);
        _transform.SetParent(nightVisionLight, ent);
        _transform.SetWorldRotation(nightVisionLight, _transform.GetWorldRotation(ent));
        ent.Comp.LocalLightEntity = nightVisionLight;
    }

    private void NightVisionOff(Entity<CE14NightVisionComponent> ent)
    {
        QueueDel(ent.Comp.LocalLightEntity);
        ent.Comp.LocalLightEntity = null;
    }

    private void NightVisionToggle(Entity<CE14NightVisionComponent> ent)
    {
        if (ent.Comp.LocalLightEntity == null)
        {
            NightVisionOn(ent);
        }
        else
        {
            NightVisionOff(ent);
        }
    }
}
