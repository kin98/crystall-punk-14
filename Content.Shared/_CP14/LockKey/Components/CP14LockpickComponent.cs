using Robust.Shared.Audio;

namespace Content.Shared._CE14.LockKey.Components;

/// <summary>
/// A component of a lock that stores its keyhole shape, complexity, and current state.
/// </summary>
[RegisterComponent]
public sealed partial class CE14LockpickComponent : Component
{
    [DataField]
    public int Health = 10;

    [DataField]
    public TimeSpan HackTime = TimeSpan.FromSeconds(1.0f);

    [DataField]
    public SoundSpecifier SuccessSound = new SoundPathSpecifier("/Audio/_CE14/Items/lockpick_use.ogg")
    {
        Params = AudioParams.Default
        .WithVariation(0.05f)
        .WithVolume(0.5f)
    };

    [DataField]
    public SoundSpecifier FailSound = new SoundPathSpecifier("/Audio/_CE14/Items/lockpick_fail.ogg")
    {
        Params = AudioParams.Default
        .WithVariation(0.05f)
        .WithVolume(0.5f)
    };
}
