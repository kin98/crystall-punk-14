using Content.Shared._CE14.Skill;
using Content.Shared._CE14.Skill.Components;

namespace Content.Server._CE14.Skill;

public sealed partial class CE14SkillSystem : CE14SharedSkillSystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeNetworkEvent<CE14TryLearnSkillMessage>(OnClientRequestLearnSkill);
    }

    private void OnClientRequestLearnSkill(CE14TryLearnSkillMessage ev, EntitySessionEventArgs args)
    {
        var entity = GetEntity(ev.Entity);

        if (args.SenderSession.AttachedEntity != entity)
            return;

        TryLearnSkill(entity, ev.Skill);
    }
}
