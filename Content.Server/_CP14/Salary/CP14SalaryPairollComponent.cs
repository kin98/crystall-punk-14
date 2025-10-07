using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.Salary;

/// <summary>
/// Pays out the salary upon interaction, if it has accumulated for the player.
/// </summary>
[RegisterComponent, Access(typeof(CE14SalarySystem))]
public sealed partial class CE14SalaryPairollComponent : Component
{
    [DataField]
    public SoundSpecifier BuySound = new SoundPathSpecifier("/Audio/_CE14/Effects/cash.ogg")
    {
        Params = AudioParams.Default.WithVariation(0.1f),
    };

    [DataField]
    public EntProtoId BuyVisual = "CE14CashImpact";
}
