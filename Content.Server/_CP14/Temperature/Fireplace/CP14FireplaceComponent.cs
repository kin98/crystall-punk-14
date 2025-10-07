using Robust.Shared.Audio;

namespace Content.Server._CE14.Temperature.Fireplace;

/// <summary>
/// component for player-controlled fire. Can be fueled.
/// </summary>

[RegisterComponent, Access(typeof(CE14FireplaceSystem))]
public sealed partial class CE14FireplaceComponent : Component
{
    [DataField]
    public string ContainerId = "storagebase";

    /// <summary>
    /// The abstract amount of fuel that is used to keep a fire burning
    /// </summary>
    [DataField]
    public float MaxFuelLimit = 100f;

    /// <summary>
    /// how much the flame grows or dies out with the presence or absence of fuel
    /// </summary>
    [DataField]
    public float FireFadeDelta = 0.2f;

    /// <summary>
    /// current fuel quantity
    /// </summary>
    [DataField]
    public float Fuel = 10f;

    /// <summary>
    /// how much fuel is wasted every "UpdateFrequency"
    /// </summary>
    [DataField]
    public float FuelDrainingPerUpdate = 1f;

    [DataField]
    public bool DeleteOnEmpty = false;

    [DataField]
    public TimeSpan UpdateFrequency = TimeSpan.FromSeconds(1f);

    [DataField]
    public TimeSpan NextUpdateTime = TimeSpan.Zero;

    [DataField]
    public SoundSpecifier InsertFuelSound = new SoundPathSpecifier("/Audio/_CE14/Items/campfire_whoosh.ogg");
}
