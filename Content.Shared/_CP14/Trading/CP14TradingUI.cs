using Robust.Shared.Serialization;

namespace Content.Shared._CE14.Trading;

[Serializable, NetSerializable]
public enum CE14TradingUiKey
{
    Buy,
    Sell,
}

[Serializable, NetSerializable]
public sealed class CE14TradingPlatformUiState(NetEntity platform) : BoundUserInterfaceState
{
    public NetEntity Platform = platform;
}

[Serializable, NetSerializable]
public sealed class CE14SellingPlatformUiState(NetEntity platform, int price) : BoundUserInterfaceState
{
    public NetEntity Platform = platform;
    public int Price = price;
}

[Serializable, NetSerializable]
public readonly struct CE14TradingProductEntry
{
}
