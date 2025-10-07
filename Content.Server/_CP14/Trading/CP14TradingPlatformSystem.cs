using Content.Server._CE14.Currency;
using Content.Server.Cargo.Systems;
using Content.Shared._CE14.Trading;
using Content.Shared._CE14.Trading.Components;
using Content.Shared._CE14.Trading.Prototypes;
using Content.Shared._CE14.Trading.Systems;
using Content.Shared.Mobs.Components;
using Content.Shared.Placeable;
using Content.Shared.Popups;
using Content.Shared.Storage;
using Content.Shared.Storage.Components;
using Content.Shared.Tag;
using Content.Shared.UserInterface;
using Robust.Server.GameObjects;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.Trading;

public sealed partial class CE14TradingPlatformSystem : CE14SharedTradingPlatformSystem
{
    [Dependency] private readonly TagSystem _tag = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly PricingSystem _price = default!;
    [Dependency] private readonly CE14CurrencySystem _CE14Currency = default!;
    [Dependency] private readonly CE14StationEconomySystem _economy = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly UserInterfaceSystem _userInterface = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14TradingPlatformComponent, CE14TradingPositionBuyAttempt>(OnBuyAttempt);

        SubscribeLocalEvent<CE14SellingPlatformComponent, BeforeActivatableUIOpenEvent>(OnBeforeSellingUIOpen);
        SubscribeLocalEvent<CE14SellingPlatformComponent, ItemPlacedEvent>(OnItemPlaced);
        SubscribeLocalEvent<CE14SellingPlatformComponent, ItemRemovedEvent>(OnItemRemoved);

        SubscribeLocalEvent<CE14SellingPlatformComponent, CE14TradingSellAttempt>(OnSellAttempt);
        SubscribeLocalEvent<CE14SellingPlatformComponent, CE14TradingRequestSellAttempt>(OnSellRequestAttempt);
    }

    private void OnSellAttempt(Entity<CE14SellingPlatformComponent> ent, ref CE14TradingSellAttempt args)
    {
        if (!TryComp<ItemPlacerComponent>(ent, out var itemPlacer))
            return;

        double balance = 0;
        foreach (var placed in itemPlacer.PlacedEntities)
        {
            if (!CanSell(placed))
                continue;

            var price = _price.GetPrice(placed);

            if (price <= 0)
                continue;

            balance += _price.GetPrice(placed);
            QueueDel(placed);
        }

        if (balance <= 0)
            return;

        _audio.PlayPvs(ent.Comp.SellSound, Transform(ent).Coordinates);
        _CE14Currency.GenerateMoney(balance * ent.Comp.PlatformMarkupProcent, Transform(ent).Coordinates);
        SpawnAtPosition(ent.Comp.SellVisual, Transform(ent).Coordinates);

        UpdateSellingUIState(ent);
    }

    private void OnSellRequestAttempt(Entity<CE14SellingPlatformComponent> ent, ref CE14TradingRequestSellAttempt args)
    {
        if (!TryComp<ItemPlacerComponent>(ent, out var itemPlacer))
            return;

        if (!CanFulfillRequest(ent, args.Request))
            return;

        if (!Proto.TryIndex(args.Request, out var indexedRequest))
            return;

        if (!_economy.TryRerollRequest(args.Faction, args.Request))
            return;

        foreach (var req in indexedRequest.Requirements)
        {
            req.PostCraft(EntityManager, Proto, itemPlacer.PlacedEntities);
        }

        _audio.PlayPvs(ent.Comp.SellSound, Transform(ent).Coordinates);
        var price = _economy.GetPrice(indexedRequest) * ent.Comp.PlatformMarkupProcent ?? 0;
        _CE14Currency.GenerateMoney(price, Transform(ent).Coordinates);
        AddReputation(args.Actor, args.Faction, price * indexedRequest.ReputationCashback);
        SpawnAtPosition(ent.Comp.SellVisual, Transform(ent).Coordinates);

        UpdateSellingUIState(ent);
    }

    private void OnItemRemoved(Entity<CE14SellingPlatformComponent> ent, ref ItemRemovedEvent args)
    {
        UpdateSellingUIState(ent);
    }

    private void OnItemPlaced(Entity<CE14SellingPlatformComponent> ent, ref ItemPlacedEvent args)
    {
        UpdateSellingUIState(ent);
    }

    private void OnBuyAttempt(Entity<CE14TradingPlatformComponent> ent, ref CE14TradingPositionBuyAttempt args)
    {
        TryBuyPosition(args.Actor, ent, args.Position);
        UpdateTradingUIState(ent, args.Actor);
    }

    private void OnBeforeSellingUIOpen(Entity<CE14SellingPlatformComponent> ent, ref BeforeActivatableUIOpenEvent args)
    {
        UpdateSellingUIState(ent);
    }

    private void UpdateSellingUIState(Entity<CE14SellingPlatformComponent> ent)
    {
        if (!TryComp<ItemPlacerComponent>(ent, out var itemPlacer))
            return;

        //Calculate
        double balance = 0;
        foreach (var placed in itemPlacer.PlacedEntities)
        {
            if (!CanSell(placed))
                continue;

            balance += _price.GetPrice(placed);
        }

        _userInterface.SetUiState(ent.Owner, CE14TradingUiKey.Sell, new CE14SellingPlatformUiState(GetNetEntity(ent), (int)(balance * ent.Comp.PlatformMarkupProcent)));
    }

    public bool CanSell(EntityUid uid)
    {
        if (_tag.HasTag(uid, "CE14Coin")) //Boo hardcoding
            return false;
        if (HasComp<MobStateComponent>(uid))
            return false;
        if (HasComp<EntityStorageComponent>(uid))
            return false;
        if (HasComp<StorageComponent>(uid))
            return false;

        var proto = MetaData(uid).EntityPrototype;
        if (proto != null && !proto.ID.StartsWith("CE14")) //Shitfix, we dont wanna sell anything vanilla (like mob organs)
            return false;

        return true;
    }

    public bool TryBuyPosition(Entity<CE14TradingReputationComponent?> user, Entity<CE14TradingPlatformComponent> platform, ProtoId<CE14TradingPositionPrototype> position)
    {
        if (Timing.CurTime < platform.Comp.NextBuyTime)
            return false;

        if (!CanBuyPosition(user, position))
            return false;

        if (!Proto.TryIndex(position, out var indexedPosition))
            return false;

        if (!Resolve(user.Owner, ref user.Comp, false))
            return false;

        if (!TryComp<ItemPlacerComponent>(platform, out var itemPlacer))
            return false;

        //Top up balance
        double balance = 0;
        foreach (var placedEntity in itemPlacer.PlacedEntities)
        {
            if (!_tag.HasTag(placedEntity, platform.Comp.CoinTag))
                continue;
            balance += _price.GetPrice(placedEntity);
        }

        var price = _economy.GetPrice(position) * platform.Comp.PlatformMarkupProcent ?? 10000;
        if (balance < price)
        {
            // Not enough balance to buy the position
            _popup.PopupEntity(Loc.GetString("CE14-trading-failure-popup-money"), platform);
            return false;
        }

        foreach (var placedEntity in itemPlacer.PlacedEntities)
        {
            if (!_tag.HasTag(placedEntity, platform.Comp.CoinTag))
                continue;
            QueueDel(placedEntity);
        }

        balance -= price;

        platform.Comp.NextBuyTime = Timing.CurTime + TimeSpan.FromSeconds(1f);
        Dirty(platform);

        if (indexedPosition.Service is not null)
            indexedPosition.Service.Buy(EntityManager, Proto, platform);

        AddReputation(user, indexedPosition.Faction, price / 100);

        _audio.PlayPvs(platform.Comp.BuySound, Transform(platform).Coordinates);

        //return the change
        _CE14Currency.GenerateMoney(balance, Transform(platform).Coordinates);
        SpawnAtPosition(platform.Comp.BuyVisual, Transform(platform).Coordinates);
        return true;
    }
}
