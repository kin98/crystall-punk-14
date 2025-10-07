using Content.Shared._CE14.Skill;
using Content.Shared._CE14.Skill.Components;
using Content.Shared._CE14.Skill.Prototypes;
using Content.Shared._CE14.Trading.Prototypes;
using Content.Shared._CE14.Trading.Systems;
using Content.Shared._CE14.Vampire.Components;
using Content.Shared.Actions;
using Content.Shared.Body.Systems;
using Content.Shared.Buckle.Components;
using Content.Shared.DoAfter;
using Content.Shared.Examine;
using Content.Shared.FixedPoint;
using Content.Shared.Humanoid;
using Content.Shared.Jittering;
using Content.Shared.Popups;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Timing;

namespace Content.Shared._CE14.Vampire;

public abstract partial class CE14SharedVampireSystem : EntitySystem
{
    [Dependency] private readonly SharedBloodstreamSystem _bloodstream = default!;
    [Dependency] private readonly SharedActionsSystem _action = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;
    [Dependency] private readonly SharedJitteringSystem _jitter = default!;
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly CE14SharedSkillSystem _skill = default!;
    [Dependency] protected readonly IPrototypeManager Proto = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly CE14SharedTradingPlatformSystem _trade = default!;

    private readonly ProtoId<CE14SkillPointPrototype> _skillPointType = "Blood";
    private readonly ProtoId<CE14SkillPointPrototype> _memorySkillPointType = "Memory";
    private readonly ProtoId<CE14TradingFactionPrototype> _tradeFaction = "VampireMarket";

    public override void Initialize()
    {
        base.Initialize();
        InitializeSpell();

        SubscribeLocalEvent<CE14VampireComponent, MapInitEvent>(OnVampireInit);
        SubscribeLocalEvent<CE14VampireComponent, ComponentRemove>(OnVampireRemove);

        SubscribeLocalEvent<CE14VampireComponent, CE14ToggleVampireVisualsAction>(OnToggleVisuals);
        SubscribeLocalEvent<CE14VampireComponent, CE14VampireToggleVisualsDoAfter>(OnToggleDoAfter);

        SubscribeLocalEvent<CE14VampireVisualsComponent, ComponentInit>(OnVampireVisualsInit);
        SubscribeLocalEvent<CE14VampireVisualsComponent, ComponentShutdown>(OnVampireVisualsShutdown);
        SubscribeLocalEvent<CE14VampireVisualsComponent, ExaminedEvent>(OnVampireExamine);

        SubscribeLocalEvent<CE14VampireEssenceHolderComponent, ExaminedEvent>(OnEssenceHolderExamined);
    }

    private void OnEssenceHolderExamined(Entity<CE14VampireEssenceHolderComponent> ent, ref ExaminedEvent args)
    {
        if (!HasComp<CE14ShowVampireEssenceComponent>(args.Examiner))
            return;

        if (!args.IsInDetailsRange)
            return;

        args.PushMarkup(Loc.GetString("CE14-vampire-essence-holder-examine", ("essence", ent.Comp.Essence)));
    }

    protected virtual void OnVampireInit(Entity<CE14VampireComponent> ent, ref MapInitEvent args)
    {
        //Bloodstream
        _bloodstream.ChangeBloodReagent(ent.Owner, ent.Comp.NewBloodReagent);

        //Actions
        foreach (var proto in ent.Comp.ActionsProto)
        {
            EntityUid? newAction = null;
            _action.AddAction(ent, ref newAction, proto);
        }

        //Skill tree
        _skill.AddSkillPoints(ent, ent.Comp.SkillPointProto, ent.Comp.SkillPointCount, silent: true);
        _skill.AddSkillTree(ent, ent.Comp.SkillTreeProto);

        //Skill tree base nerf
        _skill.RemoveSkillPoints(ent, _memorySkillPointType, 2, true);

        //Remove blood essence
        if (TryComp<CE14VampireEssenceHolderComponent>(ent, out var essenceHolder))
        {
            essenceHolder.Essence = 0;
            Dirty(ent, essenceHolder);
        }

        //Additional trade faction
        _trade.AddReputation(ent.Owner, _tradeFaction, 1);
    }

    private void OnVampireRemove(Entity<CE14VampireComponent> ent, ref ComponentRemove args)
    {
        RemCompDeferred<CE14VampireVisualsComponent>(ent);

        //Bloodstream todo

        //Metabolism todo

        //Actions
        foreach (var action in ent.Comp.Actions)
        {
            _action.RemoveAction(ent.Owner, action);
        }

        //Skill tree
        _skill.RemoveSkillTree(ent, ent.Comp.SkillTreeProto);
        if (TryComp<CE14SkillStorageComponent>(ent, out var storage))
        {
            foreach (var skill in storage.LearnedSkills)
            {
                if (!Proto.TryIndex(skill, out var indexedSkill))
                    continue;

                if (indexedSkill.Tree == ent.Comp.SkillTreeProto)
                    _skill.TryRemoveSkill(ent, skill);
            }
        }
        _skill.RemoveSkillPoints(ent, ent.Comp.SkillPointProto, ent.Comp.SkillPointCount);
        _skill.AddSkillPoints(ent, _memorySkillPointType, 2, null, true);
    }

    private void OnToggleVisuals(Entity<CE14VampireComponent> ent, ref CE14ToggleVampireVisualsAction args)
    {
        if (_timing.IsFirstTimePredicted)
            _jitter.DoJitter(ent, ent.Comp.ToggleVisualsTime, true);

        var doAfterArgs = new DoAfterArgs(EntityManager, ent, ent.Comp.ToggleVisualsTime, new CE14VampireToggleVisualsDoAfter(), ent)
        {
            Hidden = true,
            NeedHand = false,
        };

        _doAfter.TryStartDoAfter(doAfterArgs);
    }

    private void OnToggleDoAfter(Entity<CE14VampireComponent> ent, ref CE14VampireToggleVisualsDoAfter args)
    {
        if (args.Cancelled || args.Handled)
            return;

        if (HasComp<CE14VampireVisualsComponent>(ent))
        {
            RemCompDeferred<CE14VampireVisualsComponent>(ent);
        }
        else
        {
            EnsureComp<CE14VampireVisualsComponent>(ent);
        }

        args.Handled = true;
    }

    protected virtual void OnVampireVisualsShutdown(Entity<CE14VampireVisualsComponent> vampire, ref ComponentShutdown args)
    {
        if (!EntityManager.TryGetComponent(vampire, out HumanoidAppearanceComponent? humanoidAppearance))
            return;

        humanoidAppearance.EyeColor = vampire.Comp.OriginalEyesColor;

        Dirty(vampire, humanoidAppearance);
    }

    protected virtual void OnVampireVisualsInit(Entity<CE14VampireVisualsComponent> vampire, ref ComponentInit args)
    {
        if (!EntityManager.TryGetComponent(vampire, out HumanoidAppearanceComponent? humanoidAppearance))
            return;

        vampire.Comp.OriginalEyesColor = humanoidAppearance.EyeColor;
        humanoidAppearance.EyeColor = vampire.Comp.EyesColor;

        Dirty(vampire, humanoidAppearance);
    }

    private void OnVampireExamine(Entity<CE14VampireVisualsComponent> ent, ref ExaminedEvent args)
    {
        args.PushMarkup(Loc.GetString("CE14-vampire-examine"));
    }

    public void GatherEssence(Entity<CE14VampireComponent?> vampire,
        Entity<CE14VampireEssenceHolderComponent?> victim,
        FixedPoint2 amount)
    {
        if (!Resolve(vampire, ref vampire.Comp, false))
            return;

        if (!Resolve(victim, ref victim.Comp, false))
            return;

        var extractedEssence = MathF.Min(victim.Comp.Essence.Float(), amount.Float());

        if (TryComp<BuckleComponent>(victim, out var buckle) && buckle.BuckledTo is not null)
        {
            if (TryComp<CE14VampireAltarComponent>(buckle.BuckledTo, out var altar))
            {
                extractedEssence *= altar.Multiplier;
            }
        }

        if (extractedEssence <= 0)
        {
            _popup.PopupClient(Loc.GetString("CE14-vampire-gather-essence-no-left"), victim, vampire, PopupType.SmallCaution);
            return;
        }

        _skill.AddSkillPoints(vampire, _skillPointType, extractedEssence);
        victim.Comp.Essence -= amount;

        Dirty(victim);
    }
}


public sealed partial class CE14ToggleVampireVisualsAction : InstantActionEvent;

[Serializable, NetSerializable]
public sealed partial class CE14VampireToggleVisualsDoAfter : SimpleDoAfterEvent;


// Appearance Data key
[Serializable, NetSerializable]
public enum VampireClanLevelVisuals : byte
{
    Level,
}
