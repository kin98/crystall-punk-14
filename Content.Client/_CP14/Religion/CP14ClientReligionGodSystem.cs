using Content.Shared._CE14.Religion.Components;
using Content.Shared._CE14.Religion.Prototypes;
using Content.Shared._CE14.Religion.Systems;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;

namespace Content.Client._CE14.Religion;

public sealed partial class CE14ClientReligionGodSystem : CE14SharedReligionGodSystem
{
    [Dependency] private readonly IOverlayManager _overlayMgr = default!;
    [Dependency] private readonly IPlayerManager _player = default!;

    private CE14ReligionVisionOverlay? _overlay;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14ReligionVisionComponent, LocalPlayerAttachedEvent>(OnPlayerAttached);
        SubscribeLocalEvent<CE14ReligionVisionComponent, LocalPlayerDetachedEvent>(OnPlayerDetached);
        SubscribeLocalEvent<CE14ReligionVisionComponent, ComponentInit>(OnOverlayInit);
        SubscribeLocalEvent<CE14ReligionVisionComponent, ComponentRemove>(OnOverlayRemove);
    }

    public override void SendMessageToGods(ProtoId<CE14ReligionPrototype> religion, string msg, EntityUid source) { }

    public override void Shutdown()
    {
        base.Shutdown();
        _overlayMgr.RemoveOverlay<CE14ReligionVisionOverlay>();
    }

    private void OnPlayerAttached(Entity<CE14ReligionVisionComponent> ent, ref LocalPlayerAttachedEvent args)
    {
        AddOverlay();
    }

    private void OnPlayerDetached(Entity<CE14ReligionVisionComponent> ent, ref LocalPlayerDetachedEvent args)
    {
        RemoveOverlay();
    }

    private void OnOverlayInit(Entity<CE14ReligionVisionComponent> ent, ref ComponentInit args)
    {
        var attachedEnt = _player.LocalEntity;

        if (attachedEnt != ent.Owner)
            return;

        AddOverlay();
    }

    private void OnOverlayRemove(Entity<CE14ReligionVisionComponent> ent, ref ComponentRemove args)
    {
        var attachedEnt = _player.LocalEntity;

        if (attachedEnt != ent.Owner)
            return;

        RemoveOverlay();
    }

    private void AddOverlay()
    {
        if (_overlay != null)
            return;

        _overlay = new CE14ReligionVisionOverlay();
        _overlayMgr.AddOverlay(_overlay);
    }

    private void RemoveOverlay()
    {
        if (_overlay == null)
            return;

        _overlayMgr.RemoveOverlay(_overlay);
        _overlay = null;
    }
}
