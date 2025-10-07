using System.Numerics;
using Content.Server.Destructible;
using Content.Server.Destructible.Thresholds.Behaviors;
using Content.Shared._CE14.Vampire.Components;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Server._CE14.Vampire;

[Serializable]
[DataDefinition]
public sealed partial class CE14VampireAltarExplodeBehavior : IThresholdBehavior
{
    [DataField]
    public EntProtoId Essence = "CE14BloodEssence";

    [DataField]
    public float ExtractionPercentage = 0.5f;

    public void Execute(EntityUid owner, DestructibleSystem system, EntityUid? cause = null)
    {
        if(!system.EntityManager.TryGetComponent<TransformComponent>(owner, out var transform))
            return;

        if(!system.EntityManager.TryGetComponent<CE14VampireClanHeartComponent>(owner, out var clanHeart))
            return;

        var random = IoCManager.Resolve<IRobustRandom>();

        var collected = clanHeart.CollectedEssence;
        var spawnedEssence = MathF.Floor(collected.Float() * ExtractionPercentage);
        for (var i = 0; i < spawnedEssence; i++)
        {
            system.EntityManager.SpawnAtPosition(Essence, transform.Coordinates.Offset(new Vector2(random.NextFloat(-1f, 1f), random.NextFloat(-1f, 1f))));
        }
    }
}
