using Robust.Shared.GameStates;

namespace Content.Shared._CE14.MagicEnergy.Components;

/// <summary>
/// Allows you to examine how much energy is in that object.
/// </summary>
[RegisterComponent, NetworkedComponent]
[Access(typeof(CE14SharedMagicEnergySystem))]
public sealed partial class CE14MagicEnergyExaminableComponent : Component;
