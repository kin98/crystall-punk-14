using Content.Shared._CE14.Trading.Components;
using Content.Shared._CE14.Trading.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Trading.Systems;

public abstract partial class CE14SharedStationEconomySystem : EntitySystem
{
    public int? GetPrice(ProtoId<CE14TradingPositionPrototype> position)
    {
        var query = EntityQueryEnumerator<CE14StationEconomyComponent>();

        while (query.MoveNext(out var uid, out var economy))
        {
            if (!economy.Pricing.TryGetValue(position, out var price))
                return null;

            return price;
        }

        return null;
    }

    public int? GetPrice(ProtoId<CE14TradingRequestPrototype> request)
    {
        var query = EntityQueryEnumerator<CE14StationEconomyComponent>();

        while (query.MoveNext(out var uid, out var economy))
        {
            if (!economy.RequestPricing.TryGetValue(request, out var price))
                return null;

            return price;
        }

        return null;
    }

    public HashSet<ProtoId<CE14TradingRequestPrototype>> GetRequests(ProtoId<CE14TradingFactionPrototype> faction)
    {
        var query = EntityQueryEnumerator<CE14StationEconomyComponent>();

        while (query.MoveNext(out var uid, out var economy))
        {
            if (!economy.ActiveRequests.TryGetValue(faction, out var requests))
                continue;

            return requests;
        }

        return [];
    }
}
