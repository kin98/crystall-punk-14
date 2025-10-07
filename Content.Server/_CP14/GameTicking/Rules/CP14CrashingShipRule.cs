using System.Linq;
using Content.Server._CE14.GameTicking.Rules.Components;
using Content.Server.GameTicking.Rules;
using Content.Server.Shuttles.Events;
using Content.Server.Station.Components;
using Content.Server.Station.Systems;
using Content.Shared.GameTicking.Components;
using Content.Shared.Station.Components;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Server._CE14.GameTicking.Rules;

public sealed class CE14CrashingShipRule : GameRuleSystem<CE14CrashingShipRuleComponent>
{
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly ILogManager _logManager = default!;
    [Dependency] private readonly StationSystem _station = default!;
    [Dependency] private readonly IRobustRandom _random = default!;

    private ISawmill _sawmill = default!;
    public override void Initialize()
    {
        base.Initialize();
        _sawmill = _logManager.GetSawmill("CE14_crashing_ship_rule");

        SubscribeLocalEvent<CE14CrashingShipComponent, FTLCompletedEvent>(OnFTLCompleted);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        UpdateExplosions(frameTime);
    }

    protected override void Started(EntityUid uid,
        CE14CrashingShipRuleComponent component,
        GameRuleComponent gameRule,
        GameRuleStartedEvent args)
    {
        base.Started(uid, component, gameRule, args);

        var station = _station.GetStations().First();

        var largestStationGrid = _station.GetLargestGrid(station);

        if (largestStationGrid is null)
        {
            _sawmill.Error($"Station {station} does not have a grid.");
            return;
        }

        component.StartExplosionTime += _timing.CurTime;
        component.Ship = largestStationGrid.Value;
    }

    private void OnFTLCompleted(Entity<CE14CrashingShipComponent> ent, ref FTLCompletedEvent args)
    {
        SpawnRandomExplosion(ent, ent.Comp.FinalExplosionProto, 10);
        RemCompDeferred<CE14CrashingShipComponent>(ent);
    }

    private void UpdateExplosions(float frameTime)
    {
        var ruleQuery = EntityQueryEnumerator<CE14CrashingShipRuleComponent>();
        while (ruleQuery.MoveNext(out var uid, out var rule))
        {
            if (!rule.PendingExplosions)
                continue;

            if (_timing.CurTime < rule.StartExplosionTime)
                continue;

            if (rule.Ship is null)
                continue;

            AddComp<CE14CrashingShipComponent>(rule.Ship.Value);
            rule.PendingExplosions = false;
        }

        var query = EntityQueryEnumerator<CE14CrashingShipComponent>();
        while (query.MoveNext(out var uid, out var ship))
        {
            if (_timing.CurTime < ship.NextExplosionTime)
                continue;

            ship.NextExplosionTime = _timing.CurTime + TimeSpan.FromSeconds(_random.Next(2, 10));
            SpawnRandomExplosion((uid, ship), ship.ExplosionProto, 1);
        }
    }


    private void SpawnRandomExplosion(Entity<CE14CrashingShipComponent> grid, EntProtoId explosionProto, int count)
    {
        var station = _station.GetOwningStation(grid);

        if (station is null)
            return;

        TryFindRandomTileOnStation((station.Value, Comp<StationDataComponent>(station.Value)),
            out var tile,
            out var targetGrid,
            out var targetCoords);

        for (var i = 0; i < count; i++)
        {
            Spawn(explosionProto, targetCoords);
        }
    }
}
