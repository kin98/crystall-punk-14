using Content.Shared._CE14.Demiplane;
using Robust.Client.UserInterface;

namespace Content.Client._CE14.Demiplane;

public sealed class CE14DemiplaneMapBoundUserInterface : BoundUserInterface
{
    private CE14DemiplaneMapWindow? _window;

    public CE14DemiplaneMapBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
        IoCManager.InjectDependencies(this);
    }

    protected override void Open()
    {
        base.Open();

        _window = this.CreateWindow<CE14DemiplaneMapWindow>();
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        if (_window == null || state is not CE14DemiplaneMapUiState mapState)
            return;

        _window?.UpdateState(mapState);
    }
}
