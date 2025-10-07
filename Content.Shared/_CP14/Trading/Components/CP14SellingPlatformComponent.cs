using Content.Shared._CE14.Trading.Systems;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Trading.Components;

/// <summary>
/// Allows you to sell items by overloading the platform with energy
/// </summary>
[RegisterComponent, Access(typeof(CE14SharedTradingPlatformSystem))]
public sealed partial class CE14SellingPlatformComponent : Component
{
    [DataField]
    public SoundSpecifier SellSound = new SoundPathSpecifier("/Audio/_CE14/Effects/cash.ogg")
    {
        Params = AudioParams.Default.WithVariation(0.1f),
    };

    [DataField]
    public EntProtoId SellVisual = "CE14CashImpact";

    [DataField]
    public float PlatformMarkupProcent = 1f;
}
