using Content.Server.Destructible;
using Content.Server.Destructible.Thresholds.Behaviors;

namespace Content.Server._CE14.ModularCraft;

[Serializable]
[DataDefinition]
public sealed partial class CE14ModularDisassembleBehavior : IThresholdBehavior
{
    public void Execute(EntityUid owner, DestructibleSystem system, EntityUid? cause = null)
    {
        var modular = system.EntityManager.System<CE14ModularCraftSystem>();
        modular.DisassembleModular(owner);
    }
}
