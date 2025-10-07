using System.Linq;
using Content.Shared._CE14.Actions.Components;
using Content.Shared.Examine;
using Content.Shared.Mobs;

namespace Content.Shared._CE14.Actions;

public abstract partial class CE14SharedActionSystem
{
    private void InitializeExamine()
    {
        SubscribeLocalEvent<CE14ActionManaCostComponent, ExaminedEvent>(OnManacostExamined);
        SubscribeLocalEvent<CE14ActionStaminaCostComponent, ExaminedEvent>(OnStaminaCostExamined);
        SubscribeLocalEvent<CE14ActionSkillPointCostComponent, ExaminedEvent>(OnSkillPointCostExamined);

        SubscribeLocalEvent<CE14ActionSpeakingComponent, ExaminedEvent>(OnVerbalExamined);
        SubscribeLocalEvent<CE14ActionFreeHandsRequiredComponent, ExaminedEvent>(OnSomaticExamined);
        SubscribeLocalEvent<CE14ActionMaterialCostComponent, ExaminedEvent>(OnMaterialExamined);
        SubscribeLocalEvent<CE14ActionRequiredMusicToolComponent, ExaminedEvent>(OnMusicExamined);
        SubscribeLocalEvent<CE14ActionTargetMobStatusRequiredComponent, ExaminedEvent>(OnMobStateExamined);
    }

    private void OnManacostExamined(Entity<CE14ActionManaCostComponent> ent, ref ExaminedEvent args)
    {
        args.PushMarkup($"{Loc.GetString("CE14-magic-manacost")}: [color=#5da9e8]{ent.Comp.ManaCost}[/color]", priority: 9);
    }

    private void OnStaminaCostExamined(Entity<CE14ActionStaminaCostComponent> ent, ref ExaminedEvent args)
    {
        args.PushMarkup($"{Loc.GetString("CE14-magic-staminacost")}: [color=#3fba54]{ent.Comp.Stamina}[/color]", priority: 9);
    }

    private void OnSkillPointCostExamined(Entity<CE14ActionSkillPointCostComponent> ent, ref ExaminedEvent args)
    {
        if (!_proto.TryIndex(ent.Comp.SkillPoint, out var indexedSkillPoint))
            return;

        args.PushMarkup($"{Loc.GetString("CE14-magic-skillpointcost", ("name", Loc.GetString(indexedSkillPoint.Name)), ("count", ent.Comp.Count))}", priority: 9);
    }

    private void OnVerbalExamined(Entity<CE14ActionSpeakingComponent> ent, ref ExaminedEvent args)
    {
        args.PushMarkup(Loc.GetString("CE14-magic-verbal-aspect"), 8);
    }

    private void OnSomaticExamined(Entity<CE14ActionFreeHandsRequiredComponent> ent, ref ExaminedEvent args)
    {
        args.PushMarkup(Loc.GetString("CE14-magic-somatic-aspect") + " " + ent.Comp.FreeHandRequired, 8);
    }

    private void OnMaterialExamined(Entity<CE14ActionMaterialCostComponent> ent, ref ExaminedEvent args)
    {
        if (ent.Comp.Requirement is not null)
            args.PushMarkup(Loc.GetString("CE14-magic-material-aspect") + " " + ent.Comp.Requirement.GetRequirementTitle(_proto));
    }
    private void OnMusicExamined(Entity<CE14ActionRequiredMusicToolComponent> ent, ref ExaminedEvent args)
    {
        args.PushMarkup(Loc.GetString("CE14-magic-music-aspect"));
    }

    private void OnMobStateExamined(Entity<CE14ActionTargetMobStatusRequiredComponent> ent, ref ExaminedEvent args)
    {
        var states = string.Join(", ",
            ent.Comp.AllowedStates.Select(state => state switch
        {
            MobState.Alive => Loc.GetString("CE14-magic-spell-target-mob-state-live"),
            MobState.Dead => Loc.GetString("CE14-magic-spell-target-mob-state-dead"),
            MobState.Critical => Loc.GetString("CE14-magic-spell-target-mob-state-critical")
        }));

        args.PushMarkup(Loc.GetString("CE14-magic-spell-target-mob-state", ("state", states)));
    }
}
