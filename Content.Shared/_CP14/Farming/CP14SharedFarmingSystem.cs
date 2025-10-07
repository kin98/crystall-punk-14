using Content.Shared._CE14.Farming.Components;
using Content.Shared.Chemistry.Components.SolutionManager;
using Content.Shared.Destructible;
using Content.Shared.DoAfter;
using Content.Shared.Examine;
using Content.Shared.Popups;
using Content.Shared.Tag;
using Content.Shared.Whitelist;
using Robust.Shared.Map;
using Robust.Shared.Network;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Serialization;

namespace Content.Shared._CE14.Farming;

public abstract partial class CE14SharedFarmingSystem : EntitySystem
{
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    [Dependency] private readonly EntityWhitelistSystem _whitelist = default!;
    [Dependency] private readonly TagSystem _tag = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly SharedDestructibleSystem _destructible = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;
    [Dependency] private readonly INetManager _net = default!;
    [Dependency] private readonly IRobustRandom _random = default!;

    protected EntityQuery<CE14PlantComponent> PlantQuery;
    protected EntityQuery<CE14SeedComponent> SeedQuery;
    protected EntityQuery<SolutionContainerManagerComponent> SolutionQuery;

    public override void Initialize()
    {
        base.Initialize();
        InitializeInteractions();

        PlantQuery = GetEntityQuery<CE14PlantComponent>();
        SeedQuery = GetEntityQuery<CE14SeedComponent>();
        SolutionQuery = GetEntityQuery<SolutionContainerManagerComponent>();

        SubscribeLocalEvent<CE14PlantComponent, ExaminedEvent>(OnExamine);
    }

    private void OnExamine(EntityUid uid, CE14PlantComponent component, ExaminedEvent args)
    {
        if (component.Energy <= 0)
            args.PushMarkup(Loc.GetString("CE14-farming-low-energy"));

        if (component.Resource <= 0)
            args.PushMarkup(Loc.GetString("CE14-farming-low-resources"));
    }

    public void AffectEnergy(Entity<CE14PlantComponent> ent, float energyDelta)
    {
        if (energyDelta == 0)
            return;

        ent.Comp.Energy = MathHelper.Clamp(ent.Comp.Energy + energyDelta, 0, ent.Comp.EnergyMax);
        Dirty(ent);
    }

    public void AffectResource(Entity<CE14PlantComponent> ent, float resourceDelta)
    {
        if (resourceDelta == 0)
            return;

        ent.Comp.Resource = MathHelper.Clamp(ent.Comp.Resource + resourceDelta, 0, ent.Comp.ResourceMax);
        Dirty(ent);
    }

    public void AffectGrowth(Entity<CE14PlantComponent> ent, float growthDelta)
    {
        if (growthDelta == 0)
            return;

        ent.Comp.GrowthLevel = MathHelper.Clamp01(ent.Comp.GrowthLevel + growthDelta);
        Dirty(ent);
    }

    [Serializable, NetSerializable]
    public sealed partial class CE14PlantSeedDoAfterEvent : DoAfterEvent
    {
        [DataField(required:true)]
        public NetCoordinates Coordinates;

        public CE14PlantSeedDoAfterEvent(NetCoordinates coordinates)
        {
            Coordinates = coordinates;
        }

        public override DoAfterEvent Clone() => this;
    }

    [Serializable, NetSerializable]
    public sealed partial class CE14PlantGatherDoAfterEvent : SimpleDoAfterEvent;
}
