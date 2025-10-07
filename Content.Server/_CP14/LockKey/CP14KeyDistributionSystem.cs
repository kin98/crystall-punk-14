using Content.Server.Station.Events;
using Content.Shared._CE14.LockKey;
using Content.Shared._CE14.LockKey.Components;
using Content.Shared.Station.Components;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Utility;

namespace Content.Server._CE14.LockKey;

public sealed partial class CE14KeyDistributionSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _proto = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly CE14KeyholeGenerationSystem _keyGeneration = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14AbstractKeyComponent, MapInitEvent>(OnMapInit);
    }

    private void OnMapInit(Entity<CE14AbstractKeyComponent> ent, ref MapInitEvent args)
    {
        if (!TrySetShape(ent) && ent.Comp.DeleteOnFailure)
            QueueDel(ent);
    }

    private bool TrySetShape(Entity<CE14AbstractKeyComponent> ent)
    {
        var grid = Transform(ent).GridUid;

        if (grid is null)
            return false;

        if (!TryComp<CE14KeyComponent>(ent, out var key))
            return false;

        if (!TryComp<StationMemberComponent>(grid.Value, out var member))
            return false;

        if (!TryComp<CE14StationKeyDistributionComponent>(member.Station, out var distribution))
            return false;

        var keysList = new List<ProtoId<CE14LockTypePrototype>>(distribution.Keys);
        while (keysList.Count > 0)
        {
            var randomIndex = _random.Next(keysList.Count);
            var keyA = keysList[randomIndex];

            var indexedKey = _proto.Index(keyA);

            if (indexedKey.Group != ent.Comp.Group)
            {
                keysList.RemoveAt(randomIndex);
                continue;
            }

            _keyGeneration.SetShape((ent, key), indexedKey);
            distribution.Keys.Remove(indexedKey);
            return true;
        }

        return false;
    }
}
