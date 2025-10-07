using Content.Shared._CE14.Skill;
using Content.Shared._CE14.Skill.Components;
using Content.Shared._CE14.Skill.Prototypes;
using Robust.Client.Player;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Prototypes;

namespace Content.Client._CE14.Skill;

public sealed partial class CE14ClientSkillSystem : CE14SharedSkillSystem
{
    [Dependency] private readonly IPlayerManager _playerManager = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;

    public event Action<EntityUid>? OnSkillUpdate;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14SkillStorageComponent, AfterAutoHandleStateEvent>(OnAfterAutoHandleState);
    }

    private void OnAfterAutoHandleState(Entity<CE14SkillStorageComponent> ent, ref AfterAutoHandleStateEvent args)
    {
        if (ent != _playerManager.LocalEntity)
            return;

        OnSkillUpdate?.Invoke(ent.Owner);
    }

    public void RequestSkillData()
    {
        var localPlayer = _playerManager.LocalEntity;

        if (!HasComp<CE14SkillStorageComponent>(localPlayer))
            return;

        OnSkillUpdate?.Invoke(localPlayer.Value);
    }

    public void RequestLearnSkill(EntityUid? target, CE14SkillPrototype? skill)
    {
        if (skill == null || target == null)
            return;

        var netEv = new CE14TryLearnSkillMessage(GetNetEntity(target.Value), skill.ID);
        RaiseNetworkEvent(netEv);

        if (_proto.TryIndex(skill.Tree, out var indexedTree))
        {
            _audio.PlayGlobal(indexedTree.LearnSound, target.Value, AudioParams.Default.WithVolume(6f));
        }
    }
}
