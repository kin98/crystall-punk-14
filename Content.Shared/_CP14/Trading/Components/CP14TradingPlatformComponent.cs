using Content.Shared._CE14.Trading.Systems;
using Content.Shared.Tag;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Trading.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState, Access(typeof(CE14SharedTradingPlatformSystem))]
public sealed partial class CE14TradingPlatformComponent : Component
{
    [DataField, AutoNetworkedField]
    public TimeSpan NextBuyTime = TimeSpan.Zero;

    [DataField]
    public SoundSpecifier BuySound = new SoundPathSpecifier("/Audio/_CE14/Effects/cash.ogg")
    {
        Params = AudioParams.Default.WithVariation(0.1f)
    };

    [DataField]
    public ProtoId<TagPrototype> CoinTag = "CE14Coin";

    [DataField]
    public EntProtoId BuyVisual = "CE14CashImpact";


    [DataField]
    public float PlatformMarkupProcent = 1f;
}
