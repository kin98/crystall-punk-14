using Content.Shared._CE14.Religion.Systems;
using Content.Shared.FixedPoint;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Religion.Components;

/// <summary>
/// Disables standard communication. Instead, attempts to say anything will consume mana, will be limited by the zone
/// of influence of religion, and will be spoken through the created entity of the “speaker.”
/// </summary>
[RegisterComponent, NetworkedComponent, Access(typeof(CE14SharedReligionGodSystem))]
public sealed partial class CE14ReligionSpeakerComponent : Component
{
    [DataField]
    public FixedPoint2 ManaCost = 5f;

    [DataField(required: true)]
    public EntProtoId Speaker;

    /// <summary>
    /// You can only talk within the sphere of influence of religion.
    /// </summary>
    [DataField]
    public bool RestrictedReligionZone = true;
}


