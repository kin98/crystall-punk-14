using Content.Shared._CE14.Religion.Prototypes;
using Content.Shared._CE14.Religion.Systems;
using Content.Shared.FixedPoint;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Religion.Components;

/// <summary>
/// Determines whether the entity is a follower of God, or may never be able to become one
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState, Access(typeof(CE14SharedReligionGodSystem))]
public sealed partial class CE14ReligionFollowerComponent : Component
{
    [DataField, AutoNetworkedField]
    public ProtoId<CE14ReligionPrototype>? Religion;

    [DataField, AutoNetworkedField]
    public HashSet<ProtoId<CE14ReligionPrototype>> RejectedReligions = new();

    [DataField]
    public EntProtoId RenounceActionProto = "CE14ActionRenounceFromGod";

    [DataField]
    public EntProtoId AppealToGofProto = "CE14ActionAppealToGod";

    [DataField]
    public EntityUid? RenounceAction;

    [DataField]
    public EntityUid? AppealAction;

    /// <summary>
    /// how much energy does the entity transfer to its god
    /// </summary>
    [DataField]
    public FixedPoint2 EnergyToGodTransfer = 0.5f;

    /// <summary>
    /// how often will the entity transfer mana to its patreon
    /// </summary>
    [DataField]
    public float ManaTransferDelay = 3f;

    /// <summary>
    /// the time of the next magic energy change
    /// </summary>
    [DataField]
    public TimeSpan NextUpdateTime { get; set; } = TimeSpan.Zero;
}
