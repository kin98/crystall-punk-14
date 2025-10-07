using Content.Shared._CE14.Trading.Components;
using Content.Shared._CE14.Trading.Prototypes;
using Content.Shared.Interaction.Events;
using Content.Shared.Placeable;
using Content.Shared.Popups;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Network;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Timing;

namespace Content.Shared._CE14.Trading.Systems;

public abstract partial class CE14SharedTradingPlatformSystem : EntitySystem
{
    [Dependency] private readonly SharedUserInterfaceSystem _userInterface = default!;
    [Dependency] protected readonly IPrototypeManager Proto = default!;
    [Dependency] protected readonly IGameTiming Timing = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly INetManager _net = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;

    public override void Initialize()
    {
        base.Initialize();
        InitializeUI();

        SubscribeLocalEvent<CE14TradingReputationComponent, MapInitEvent>(OnReputationMapInit);
        SubscribeLocalEvent<CE14TradingContractComponent, UseInHandEvent>(OnContractUse);
    }

    private void OnReputationMapInit(Entity<CE14TradingReputationComponent> ent, ref MapInitEvent args)
    {
        foreach (var faction in Proto.EnumeratePrototypes<CE14TradingFactionPrototype>())
        {
            if (faction.RoundStart is not null)
            {
                ent.Comp.Reputation[faction] = ent.Comp.Reputation.GetValueOrDefault(faction, 0f) + faction.RoundStart.Value;
            }
        }
        Dirty(ent);
    }

    private void OnContractUse(Entity<CE14TradingContractComponent> ent, ref UseInHandEvent args)
    {
        if (args.Handled)
            return;
        if (!Proto.TryIndex(ent.Comp.Faction, out var indexedFaction))
            return;

        args.Handled = true;

        var repComp = EnsureComp<CE14TradingReputationComponent>(args.User);
        repComp.Reputation.TryAdd(ent.Comp.Faction, 0);
        _audio.PlayLocal(new SoundCollectionSpecifier("CE14CoinImpact"), args.User, args.User);
        _popup.PopupClient(Loc.GetString("CE14-trading-contract-use", ("name", Loc.GetString(indexedFaction.Name))), args.User, args.User);

        if (_net.IsServer)
            QueueDel(ent);
    }

    public bool CanBuyPosition(Entity<CE14TradingReputationComponent?> user, ProtoId<CE14TradingPositionPrototype> position)
    {
        if (!Resolve(user.Owner, ref user.Comp, false))
            return false;
        if (!Proto.TryIndex(position, out var indexedPosition))
            return false;

        if (user.Comp.Reputation[indexedPosition.Faction] < indexedPosition.ReputationLevel)
            return false;

        return true;
    }

    public void AddReputation(Entity<CE14TradingReputationComponent?> user,
        ProtoId<CE14TradingFactionPrototype> faction, float rep)
    {
        if (!Resolve(user.Owner, ref user.Comp, false))
            return;

        if (!user.Comp.Reputation.ContainsKey(faction))
            user.Comp.Reputation.Add(faction, rep);
        else
            user.Comp.Reputation[faction] += rep;

        Dirty(user);
    }

    public bool CanFulfillRequest(EntityUid platform, ProtoId<CE14TradingRequestPrototype> request)
    {
        if (!TryComp<ItemPlacerComponent>(platform, out var itemPlacer))
            return false;

        if (!Proto.TryIndex(request, out var indexedRequest))
            return false;

        foreach (var requirement in indexedRequest.Requirements)
        {
            if (!requirement.CheckRequirement(EntityManager, Proto, itemPlacer.PlacedEntities))
                return false;
        }

        return true;
    }
}

[Serializable, NetSerializable]
public sealed class CE14TradingPositionBuyAttempt(ProtoId<CE14TradingPositionPrototype> position) : BoundUserInterfaceMessage
{
    public readonly ProtoId<CE14TradingPositionPrototype> Position = position;
}

[Serializable, NetSerializable]
public sealed class CE14TradingRequestSellAttempt(ProtoId<CE14TradingRequestPrototype> request, ProtoId<CE14TradingFactionPrototype> faction) : BoundUserInterfaceMessage
{
    public readonly ProtoId<CE14TradingRequestPrototype> Request = request;
    public readonly ProtoId<CE14TradingFactionPrototype> Faction = faction;
}


[Serializable, NetSerializable]
public sealed class CE14TradingSellAttempt : BoundUserInterfaceMessage
{
}
