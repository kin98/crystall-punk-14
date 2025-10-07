using Content.Shared._CE14.MagicSpell.Spells;
using Content.Shared.Actions;
using Content.Shared.Actions.Components;

namespace Content.Shared._CE14.Actions;

public abstract partial class CE14SharedActionSystem
{
    private void InitializeModularEffects()
    {
        SubscribeLocalEvent<TransformComponent, CE14ActionStartDoAfterEvent>(OnActionTelegraphy);

        SubscribeLocalEvent<TransformComponent, CE14InstantModularEffectEvent>(OnInstantCast);
        SubscribeLocalEvent<TransformComponent, CE14WorldTargetModularEffectEvent>(OnWorldTargetCast);
        SubscribeLocalEvent<TransformComponent, CE14EntityTargetModularEffectEvent>(OnEntityTargetCast);
    }

    private void OnActionTelegraphy(Entity<TransformComponent> ent, ref CE14ActionStartDoAfterEvent args)
    {
        if (!_timing.IsFirstTimePredicted)
            return;

        var performer = GetEntity(args.Performer);
        var action = GetEntity(args.Input.Action);
        var target = GetEntity(args.Input.EntityTarget);
        var targetPosition = GetCoordinates(args.Input.EntityCoordinatesTarget);

        if (!TryComp<ActionComponent>(action, out var actionComp))
            return;

        //Instant
        if (TryComp<InstantActionComponent>(action, out var instant) && instant.Event is CE14InstantModularEffectEvent instantModular)
        {
            var spellArgs = new CE14SpellEffectBaseArgs(performer, actionComp.Container, performer, Transform(performer).Coordinates);

            foreach (var effect in instantModular.TelegraphyEffects)
            {
                effect.Effect(EntityManager, spellArgs);
            }
        }

        //World Target
        if (TryComp<WorldTargetActionComponent>(action, out var worldTarget) && worldTarget.Event is CE14WorldTargetModularEffectEvent worldModular && targetPosition is not null)
        {
            var spellArgs = new CE14SpellEffectBaseArgs(performer, actionComp.Container, null, targetPosition.Value);

            foreach (var effect in worldModular.TelegraphyEffects)
            {
                effect.Effect(EntityManager, spellArgs);
            }
        }

        //Entity Target
        if (TryComp<EntityTargetActionComponent>(action, out var entityTarget) && entityTarget.Event is CE14EntityTargetModularEffectEvent entityModular && target is not null)
        {
            var spellArgs = new CE14SpellEffectBaseArgs(performer, actionComp.Container, target, Transform(target.Value).Coordinates);

            foreach (var effect in entityModular.TelegraphyEffects)
            {
                effect.Effect(EntityManager, spellArgs);
            }
        }
    }

    private void OnInstantCast(Entity<TransformComponent> ent, ref CE14InstantModularEffectEvent args)
    {
        if (!_timing.IsFirstTimePredicted)
            return;

        var spellArgs = new CE14SpellEffectBaseArgs(args.Performer, args.Action.Comp.Container, args.Performer, Transform(args.Performer).Coordinates);

        foreach (var effect in args.Effects)
        {
            effect.Effect(EntityManager, spellArgs);
        }

        args.Handled = true;
    }

    private void OnWorldTargetCast(Entity<TransformComponent> ent, ref CE14WorldTargetModularEffectEvent args)
    {
        if (!_timing.IsFirstTimePredicted)
            return;

        var spellArgs = new CE14SpellEffectBaseArgs(args.Performer, args.Action.Comp.Container, null, args.Target);

        foreach (var effect in args.Effects)
        {
            effect.Effect(EntityManager, spellArgs);
        }

        args.Handled = true;
    }

    private void OnEntityTargetCast(Entity<TransformComponent> ent, ref CE14EntityTargetModularEffectEvent args)
    {
        if (!_timing.IsFirstTimePredicted)
            return;

        var spellArgs = new CE14SpellEffectBaseArgs(args.Performer, args.Action.Comp.Container, args.Target, Transform(args.Target).Coordinates);

        foreach (var effect in args.Effects)
        {
            effect.Effect(EntityManager, spellArgs);
        }

        args.Handled = true;
    }
}

public sealed partial class CE14InstantModularEffectEvent : InstantActionEvent
{
    /// <summary>
    /// Effects that will trigger at the beginning of the cast, before mana is spent. Should have no gameplay importance, just special effects, popups and sounds.
    /// </summary>
    [DataField]
    public List<CE14SpellEffect> TelegraphyEffects = new();

    [DataField]
    public List<CE14SpellEffect> Effects = new();
}

public sealed partial class CE14WorldTargetModularEffectEvent : WorldTargetActionEvent
{
    /// <summary>
    /// Effects that will trigger at the beginning of the cast, before mana is spent. Should have no gameplay importance, just special effects, popups and sounds.
    /// </summary>
    [DataField]
    public List<CE14SpellEffect> TelegraphyEffects = new();

    [DataField]
    public List<CE14SpellEffect> Effects = new();
}

public sealed partial class CE14EntityTargetModularEffectEvent : EntityTargetActionEvent
{
    /// <summary>
    /// Effects that will trigger at the beginning of the cast, before mana is spent. Should have no gameplay importance, just special effects, popups and sounds.
    /// </summary>
    [DataField]
    public List<CE14SpellEffect> TelegraphyEffects = new();

    [DataField]
    public List<CE14SpellEffect> Effects = new();
}
