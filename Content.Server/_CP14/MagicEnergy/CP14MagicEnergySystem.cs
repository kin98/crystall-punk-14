using Content.Shared._CE14.MagicEnergy;
using Content.Shared._CE14.MagicEnergy.Components;
using Content.Shared.Cargo;
using Robust.Shared.Timing;

namespace Content.Server._CE14.MagicEnergy;

public sealed partial class CE14MagicEnergySystem : CE14SharedMagicEnergySystem
{
    [Dependency] private readonly IGameTiming _gameTiming = default!;
    [Dependency] private readonly CE14MagicEnergyCrystalSlotSystem _magicSlot = default!;

    public override void Initialize()
    {
        base.Initialize();

        InitializeDraw();
        InitializePortRelay();

        SubscribeLocalEvent<CE14MagicEnergyContainerComponent, PriceCalculationEvent>(OnMagicEnergyPriceCalculation);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        UpdateDraw(frameTime);
        UpdatePortRelay(frameTime);
    }

    private void OnMagicEnergyPriceCalculation(Entity<CE14MagicEnergyContainerComponent> ent, ref PriceCalculationEvent args)
    {
        args.Price += (double)(ent.Comp.Energy * 0.1f);
    }
}
