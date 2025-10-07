using Content.Shared._CE14.Religion.Components;
using Content.Shared._CE14.Religion.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE14.Religion.Systems;

public abstract partial class CE14SharedReligionGodSystem : EntitySystem
{
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;

    public override void Initialize()
    {
        base.Initialize();
        InitializeObservation();
        InitializeFollowers();
        InitializeAltars();
    }

    public HashSet<Entity<CE14ReligionEntityComponent>> GetGods(ProtoId<CE14ReligionPrototype> religion)
    {
        HashSet<Entity<CE14ReligionEntityComponent>> gods = new();

        var query = EntityQueryEnumerator<CE14ReligionEntityComponent>();
        while (query.MoveNext(out var uid, out var god))
        {
            if (god.Religion != religion)
                continue;

            gods.Add(new Entity<CE14ReligionEntityComponent>(uid, god));
        }

        return gods;
    }

    public abstract void SendMessageToGods(ProtoId<CE14ReligionPrototype> religion, string msg, EntityUid source);
}

/// <summary>
/// It is invoked on altars and followers when they change their religion.
/// </summary>
public sealed class CE14ReligionChangedEvent(ProtoId<CE14ReligionPrototype>? oldRel, ProtoId<CE14ReligionPrototype>? newRel) : EntityEventArgs
{
    public ProtoId<CE14ReligionPrototype>? OldReligion = oldRel;
    public ProtoId<CE14ReligionPrototype>? NewReligion = newRel;
}
