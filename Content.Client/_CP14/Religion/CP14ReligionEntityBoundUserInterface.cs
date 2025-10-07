using Content.Shared._CE14.Religion.Systems;
using Robust.Client.UserInterface;

namespace Content.Client._CE14.Religion;

public sealed class CE14ReligionEntityBoundUserInterface : BoundUserInterface
{
    private CE14ReligionEntityWindow? _window;

    public CE14ReligionEntityBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();

        _window = this.CreateWindow<CE14ReligionEntityWindow>();

        _window.OnTeleportAttempt += netId => SendMessage(new CE14ReligionEntityTeleportAttempt(netId));
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        if (_window == null || state is not CE14ReligionEntityUiState mapState)
            return;

        _window?.UpdateState(mapState);
    }
}
