using Content.Shared._CE14.Trading;
using Content.Shared._CE14.Trading.Systems;
using Robust.Client.UserInterface;

namespace Content.Client._CE14.Trading.Selling;

public sealed class CE14SellingPlatformBoundUserInterface(EntityUid owner, Enum uiKey) : BoundUserInterface(owner, uiKey)
{
    private CE14SellingPlatformWindow? _window;

    protected override void Open()
    {
        base.Open();

        _window = this.CreateWindow<CE14SellingPlatformWindow>();

        _window.OnSell += () => SendMessage(new CE14TradingSellAttempt());
        _window.OnRequestSell += pair => SendMessage(new CE14TradingRequestSellAttempt(pair.Item1, pair.Item2));
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        switch (state)
        {
            case CE14SellingPlatformUiState storeState:
                _window?.UpdateState(storeState);
                break;
        }
    }
}
