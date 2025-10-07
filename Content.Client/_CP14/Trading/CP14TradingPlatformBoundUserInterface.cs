using Content.Shared._CE14.Trading;
using Content.Shared._CE14.Trading.Systems;
using Robust.Client.UserInterface;

namespace Content.Client._CE14.Trading;

public sealed class CE14TradingPlatformBoundUserInterface(EntityUid owner, Enum uiKey) : BoundUserInterface(owner, uiKey)
{
    private CE14TradingPlatformWindow? _window;

    protected override void Open()
    {
        base.Open();

        _window = this.CreateWindow<CE14TradingPlatformWindow>();

        _window.OnBuy += pos => SendMessage(new CE14TradingPositionBuyAttempt(pos));
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        switch (state)
        {
            case CE14TradingPlatformUiState storeState:
                _window?.UpdateState(storeState);
                break;
        }
    }
}
