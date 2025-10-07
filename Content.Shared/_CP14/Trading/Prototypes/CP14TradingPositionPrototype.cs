using System.Numerics;
using Content.Shared.FixedPoint;
using JetBrains.Annotations;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._CE14.Trading.Prototypes;

[Prototype("CE14TradingPosition")]
public sealed partial class CE14TradingPositionPrototype : IPrototype
{
    [IdDataField] public string ID { get; private set; } = default!;

    /// <summary>
    /// Service Title. If you leave null, the name will try to generate from first Service.GetName()
    /// </summary>
    [DataField]
    public LocId? Name;

    /// <summary>
    /// Service Description. If you leave null, the description will try to generate from first Service.GetDescription()
    /// </summary>
    [DataField]
    public LocId? Desc;

    /// <summary>
    /// Icon for UI. If you leave null, the icon will try to generate from first Service.GetTexture()
    /// </summary>
    [DataField(required: true)]
    public SpriteSpecifier Icon = default!;

    [DataField(required: true)]
    public ProtoId<CE14TradingFactionPrototype> Faction;

    [DataField]
    public FixedPoint2 ReputationLevel = 0f;

    [DataField(required: true)]
    public float UiPosition = default!;

    [DataField(required: true)]
    public CE14StoreBuyService? Service = null;

    [DataField]
    public int PriceMarkup = 1;

    /// <summary>
    /// each round prices will differ within +X percent of the calculated value
    /// </summary>
    [DataField]
    public float PriceFluctuation = 0.6f;
}

[ImplicitDataDefinitionForInheritors]
[MeansImplicitUse]
public abstract partial class CE14StoreBuyService
{
    public abstract void Buy(EntityManager entManager, IPrototypeManager prototype,  EntityUid platform);

    public abstract string GetName(IPrototypeManager protoMan);

    public abstract string GetDesc(IPrototypeManager protoMan);
}
