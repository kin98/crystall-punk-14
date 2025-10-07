using Content.Shared.StatusIcon;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Vampire;

[Prototype("CE14VampireFaction")]
public sealed partial class CE14VampireFactionPrototype : IPrototype
{
    [IdDataField] public string ID { get; private set; } = default!;

    [DataField(required: true)]
    public LocId Name = string.Empty;

    [DataField(required: true)]
    public ProtoId<FactionIconPrototype> FactionIcon;

    [DataField(required: true)]
    public string SingletonTeleportKey = string.Empty;
}
