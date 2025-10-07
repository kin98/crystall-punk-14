using Content.Shared._CE14.Trading.Prototypes;
using Content.Shared._CE14.Trading.Systems;
using Content.Shared.FixedPoint;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Trading.Components;

/// <summary>
/// Reflects the entity's level of reputation, debts, and balance sheet in the “outside” world.
/// Used for personal progression in trading systems
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState, Access(typeof(CE14SharedTradingPlatformSystem))]
public sealed partial class CE14TradingReputationComponent : Component
{
    /// <summary>
    /// is both a reputation counter for each faction and an indicator of whether that faction is unlocked for that player.
    /// </summary>
    [DataField, AutoNetworkedField]
    public Dictionary<ProtoId<CE14TradingFactionPrototype>, FixedPoint2> Reputation = new();
}
