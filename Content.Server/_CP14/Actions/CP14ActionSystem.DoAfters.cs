using Content.Server.Chat.Systems;
using Content.Shared._CE14.Actions;
using Content.Shared._CE14.Actions.Components;
using Content.Shared.Actions.Events;

namespace Content.Server._CE14.Actions;

public sealed partial class CE14ActionSystem
{
    private void InitializeDoAfter()
    {
        SubscribeLocalEvent<CE14ActionSpeakingComponent, CE14ActionStartDoAfterEvent>(OnVerbalActionStarted);
        SubscribeLocalEvent<CE14ActionSpeakingComponent, ActionDoAfterEvent>(OnVerbalActionPerformed);

        SubscribeLocalEvent<CE14ActionEmotingComponent, CE14ActionStartDoAfterEvent>(OnEmoteActionStarted);
        SubscribeLocalEvent<CE14ActionEmotingComponent, ActionDoAfterEvent>(OnEmoteActionPerformed);

        SubscribeLocalEvent<CE14ActionDoAfterVisualsComponent, CE14ActionStartDoAfterEvent>(OnSpawnMagicVisualEffect);
        SubscribeLocalEvent<CE14ActionDoAfterVisualsComponent, ActionDoAfterEvent>(OnDespawnMagicVisualEffect);
    }

    private void OnVerbalActionStarted(Entity<CE14ActionSpeakingComponent> ent, ref CE14ActionStartDoAfterEvent args)
    {
        var performer = GetEntity(args.Performer);
        _chat.TrySendInGameICMessage(performer, ent.Comp.StartSpeech, ent.Comp.Whisper ? InGameICChatType.Whisper : InGameICChatType.Speak, true);
    }

    private void OnEmoteActionStarted(Entity<CE14ActionEmotingComponent> ent, ref CE14ActionStartDoAfterEvent args)
    {
        var performer = GetEntity(args.Performer);
        _chat.TrySendInGameICMessage(performer, Loc.GetString(ent.Comp.StartEmote), InGameICChatType.Emote, true);
    }

    private void OnVerbalActionPerformed(Entity<CE14ActionSpeakingComponent> ent, ref ActionDoAfterEvent args)
    {
        if (args.Cancelled)
            return;

        if (!args.Handled)
            return;

        var performer = GetEntity(args.Performer);
        _chat.TrySendInGameICMessage(performer, ent.Comp.EndSpeech, ent.Comp.Whisper ? InGameICChatType.Whisper : InGameICChatType.Speak, true);
    }

    private void OnEmoteActionPerformed(Entity<CE14ActionEmotingComponent> ent, ref ActionDoAfterEvent args)
    {
        if (args.Cancelled)
            return;

        if (!args.Handled)
            return;

        var performer = GetEntity(args.Performer);
        _chat.TrySendInGameICMessage(performer, Loc.GetString(ent.Comp.EndEmote), InGameICChatType.Emote, true);
    }

    private void OnSpawnMagicVisualEffect(Entity<CE14ActionDoAfterVisualsComponent> ent, ref CE14ActionStartDoAfterEvent args)
    {
        QueueDel(ent.Comp.SpawnedEntity);

        var performer = GetEntity(args.Performer);
        var vfx = SpawnAttachedTo(ent.Comp.Proto, Transform(performer).Coordinates);
        _transform.SetParent(vfx, performer);
        ent.Comp.SpawnedEntity = vfx;
    }

    private void OnDespawnMagicVisualEffect(Entity<CE14ActionDoAfterVisualsComponent> ent, ref ActionDoAfterEvent args)
    {
        if (args.Repeat)
            return;

        QueueDel(ent.Comp.SpawnedEntity);
        ent.Comp.SpawnedEntity = null;
    }
}
