using Content.Shared._CE14.ModularCraft;
using Content.Shared._CE14.ModularCraft.Components;
using Content.Shared._CE14.ModularCraft.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server._CE14.ModularCraft.Modifiers;

public sealed partial class Inherit : CE14ModularCraftModifier
{
    [DataField(required: true)]
    public List<ProtoId<CE14ModularCraftPartPrototype>> CopyFrom = new();

    public override void Effect(EntityManager entManager, Entity<CE14ModularCraftStartPointComponent> start, Entity<CE14ModularCraftPartComponent>? part)
    {
        var prototypeManager = IoCManager.Resolve<IPrototypeManager>();

        foreach (var copy in CopyFrom)
        {
            foreach (var modifier in prototypeManager.Index(copy).Modifiers)
            {
                modifier.Effect(entManager, start, part);
            }
        }
    }
}
