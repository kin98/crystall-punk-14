using Content.Shared._CE14.DayCycle;
using Content.Shared._CE14.Farming.Components;
using Content.Shared.Chemistry.Components.SolutionManager;

namespace Content.Server._CE14.Farming;

public sealed partial class CE14FarmingSystem
{
    [Dependency] private readonly CE14DayCycleSystem _dayCycle = default!;
    private void InitializeResources()
    {
        SubscribeLocalEvent<CE14PlantEnergyFromLightComponent, CE14PlantUpdateEvent>(OnTakeEnergyFromLight);
        SubscribeLocalEvent<CE14PlantMetabolizerComponent, CE14PlantUpdateEvent>(OnPlantMetabolizing);

        SubscribeLocalEvent<CE14PlantGrowingComponent, CE14AfterPlantUpdateEvent>(OnPlantGrowing);
    }

    private void OnTakeEnergyFromLight(Entity<CE14PlantEnergyFromLightComponent> regeneration, ref CE14PlantUpdateEvent args)
    {
        var gainEnergy = false;
        var daylight = _dayCycle.UnderSunlight(regeneration);

        if (regeneration.Comp.Daytime && daylight)
            gainEnergy = true;

        if (regeneration.Comp.Nighttime && !daylight)
            gainEnergy = true;

        if (gainEnergy)
            args.EnergyDelta += regeneration.Comp.Energy;
    }

    private void OnPlantGrowing(Entity<CE14PlantGrowingComponent> growing, ref CE14AfterPlantUpdateEvent args)
    {
        if (args.Plant.Comp.Energy < growing.Comp.EnergyCost)
            return;

        if (args.Plant.Comp.Resource < growing.Comp.ResourceCost)
            return;

        if (args.Plant.Comp.GrowthLevel >= 1)
            return;

        AffectEnergy(args.Plant, -growing.Comp.EnergyCost);
        AffectResource(args.Plant, -growing.Comp.ResourceCost);

        AffectGrowth(args.Plant, growing.Comp.GrowthPerUpdate);
    }

    private void OnPlantMetabolizing(Entity<CE14PlantMetabolizerComponent> ent, ref CE14PlantUpdateEvent args)
    {
        if (!PlantQuery.TryComp(ent, out var plant) ||
            !SolutionQuery.TryComp(args.Plant, out var solmanager))
            return;

        var solEntity = new Entity<SolutionContainerManagerComponent?>(args.Plant, solmanager);
        if (!_solutionContainer.TryGetSolution(solEntity, plant.Solution, out var soln, out _))
            return;

        if (!_proto.TryIndex(ent.Comp.MetabolizerId, out var metabolizer))
            return;

        var splitted = _solutionContainer.SplitSolution(soln.Value, ent.Comp.SolutionPerUpdate);
        foreach (var reagent in splitted)
        {
            if (!metabolizer.Metabolization.ContainsKey(reagent.Reagent.ToString()))
                continue;

            foreach (var effect in metabolizer.Metabolization[reagent.Reagent.ToString()])
            {
                effect.Effect((ent, plant), reagent.Quantity, EntityManager);
            }
        }
    }
}
