using System.Linq;
using Content.Shared._CE14.Skill.Components;
using Content.Shared._CE14.Skill.Prototypes;
using Content.Shared.Administration;
using Content.Shared.Administration.Managers;
using Content.Shared.FixedPoint;
using Content.Shared.Verbs;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._CE14.Skill;

public abstract partial class CE14SharedSkillSystem
{
    [Dependency] private readonly ISharedAdminManager _admin = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;

    private IEnumerable<CE14SkillPrototype>? _allSkills;
    private IEnumerable<CE14SkillTreePrototype>? _allTrees;
    private void InitializeAdmin()
    {
        SubscribeLocalEvent<CE14SkillStorageComponent, GetVerbsEvent<Verb>>(OnGetAdminVerbs);

        SubscribeLocalEvent<PrototypesReloadedEventArgs>(OnPrototypeReloaded);

        UpdateCachedSkill();
    }

    private void OnPrototypeReloaded(PrototypesReloadedEventArgs ev)
    {
        if (!ev.WasModified<CE14SkillPrototype>())
            return;

        UpdateCachedSkill();
    }

    private void UpdateCachedSkill()
    {
        _allSkills = _proto.EnumeratePrototypes<CE14SkillPrototype>();
        _allTrees = _proto.EnumeratePrototypes<CE14SkillTreePrototype>();
    }


    private void OnGetAdminVerbs(Entity<CE14SkillStorageComponent> ent, ref GetVerbsEvent<Verb> args)
    {
        if (!_admin.HasAdminFlag(args.User, AdminFlags.Admin))
            return;

        if (_allSkills is null || _allTrees is null)
            return;

        var target = args.Target;

        //Reset/Remove All Skills
        args.Verbs.Add(new Verb
        {
            Text = "Reset skills",
            Message = "Remove all learned skills",
            Category = VerbCategory.Debug,
            Icon = new SpriteSpecifier.Rsi(new("/Textures/_CE14/Interface/Misc/reroll.rsi"), "reroll"),
            Act = () =>
            {
                TryResetSkills(target);
            },
        });
    }
}
