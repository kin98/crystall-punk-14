using Content.Shared._CE14.Trading.Components;
using Content.Shared._CE14.Trading.Prototypes;
using Content.Shared.UserInterface;

namespace Content.Shared._CE14.Trading.Systems;

public abstract partial class CE14SharedTradingPlatformSystem
{
    private void InitializeUI()
    {
        SubscribeLocalEvent<CE14TradingPlatformComponent, BeforeActivatableUIOpenEvent>(OnBeforeTradingUIOpen);
    }

    private void OnBeforeTradingUIOpen(Entity<CE14TradingPlatformComponent> ent, ref BeforeActivatableUIOpenEvent args)
    {
        UpdateTradingUIState(ent, args.User);
    }

    protected void UpdateTradingUIState(Entity<CE14TradingPlatformComponent> ent, EntityUid user)
    {
        _userInterface.SetUiState(ent.Owner, CE14TradingUiKey.Buy, new CE14TradingPlatformUiState(GetNetEntity(ent)));
    }

    public string GetTradeDescription(CE14TradingPositionPrototype position)
    {
        if (position.Desc != null)
            return Loc.GetString(position.Desc);

        if (position.Service is null)
            return string.Empty;

        return position.Service.GetDesc(Proto);
    }

    public string GetTradeName(CE14TradingPositionPrototype position)
    {
        if (position.Name != null)
            return Loc.GetString(position.Name);

        if (position.Service is null)
            return string.Empty;

        return position.Service.GetName(Proto);
    }
}
