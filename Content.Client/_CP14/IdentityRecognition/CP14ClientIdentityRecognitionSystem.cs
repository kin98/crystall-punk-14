using Content.Shared._CE14.IdentityRecognition;
using Content.Shared.IdentityManagement;
using Content.Shared.Mind;
using Content.Shared.Mind.Components;

namespace Content.Client._CE14.IdentityRecognition;

public sealed partial class CE14ClientIdentityRecognitionSystem : CE14SharedIdentityRecognitionSystem
{
    [Dependency] private readonly SharedMindSystem _mind = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MindContainerComponent, CE14ClientTransformNameEvent>(OnTransformSpeakerName);
    }

    private void OnTransformSpeakerName(Entity<MindContainerComponent> ent, ref CE14ClientTransformNameEvent args)
    {
        if (args.Handled)
            return;

        var mindEntity = ent.Comp.Mind;
        if (mindEntity is null)
            return;

        TryComp<CE14RememberedNamesComponent>(mindEntity.Value, out var knownNames);

        var speaker = GetEntity(args.Speaker);

        if (speaker == ent.Owner)
            return;

        if (knownNames is not null && knownNames.Names.TryGetValue(args.Speaker.Id, out var name))
        {
            args.Name = name;
        }
        else
        {
            args.Name = Identity.Name(speaker, EntityManager, ent);
        }
        args.Handled = true;
    }
}

public sealed class CE14ClientTransformNameEvent(NetEntity speaker) : EntityEventArgs
{
    public NetEntity Speaker = speaker;

    public string Name = string.Empty;

    public bool Handled { get; set; }
}
