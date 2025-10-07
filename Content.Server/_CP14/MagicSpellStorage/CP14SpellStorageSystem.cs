using Content.Server._CE14.MagicSpellStorage.Components;
using Content.Shared._CE14.MagicSpell.Events;
using Content.Shared._CE14.MagicSpellStorage;
using Content.Shared.Actions;
using Content.Shared.Damage;
using Content.Shared.Mind;

namespace Content.Server._CE14.MagicSpellStorage;

/// <summary>
/// this part of the system is responsible for storing spells in items, and the methods players use to obtain them.
/// </summary>
public sealed partial class CE14SpellStorageSystem : CE14SharedSpellStorageSystem
{
    [Dependency] private readonly ActionContainerSystem _actionContainer = default!;
    [Dependency] private readonly SharedActionsSystem _actions = default!;
    [Dependency] private readonly SharedMindSystem _mind = default!;
    [Dependency] private readonly DamageableSystem _damageable = default!;

    public override void Initialize()
    {
        InitializeAccess();

        SubscribeLocalEvent<CE14SpellStorageComponent, MapInitEvent>(OnMagicStorageInit);
        SubscribeLocalEvent<CE14SpellStorageComponent, ComponentShutdown>(OnMagicStorageShutdown);

        SubscribeLocalEvent<CE14SpellStorageUseDamageComponent, CE14SpellFromSpellStorageUsedEvent>(OnSpellUsed);
    }

    private void OnSpellUsed(Entity<CE14SpellStorageUseDamageComponent> ent, ref CE14SpellFromSpellStorageUsedEvent args)
    {
        _damageable.TryChangeDamage(ent, ent.Comp.DamagePerMana * args.Manacost);
    }

    /// <summary>
    /// When we initialize, we create action entities, and add them to this item.
    /// </summary>
    private void OnMagicStorageInit(Entity<CE14SpellStorageComponent> storage, ref MapInitEvent args)
    {
        foreach (var spell in storage.Comp.Spells)
        {
            var spellEnt = _actionContainer.AddAction(storage, spell);
            if (spellEnt is null)
                continue;

            storage.Comp.SpellEntities.Add(spellEnt.Value);
        }

        if (storage.Comp.GrantAccessToSelf)
        {
            if (!_mind.TryGetMind(storage.Owner, out var mind, out _))
                _actions.GrantActions(storage.Owner, storage.Comp.SpellEntities, storage.Owner);
            else
            {
                foreach (var spell in storage.Comp.SpellEntities)
                {
                    _actionContainer.AddAction(mind, spell);
                }
            }
        }
    }

    private void OnMagicStorageShutdown(Entity<CE14SpellStorageComponent> mStorage, ref ComponentShutdown args)
    {
        foreach (var spell in mStorage.Comp.SpellEntities)
        {
            QueueDel(spell);
        }
    }

    private bool TryGrantAccess(Entity<CE14SpellStorageComponent> storage, EntityUid user)
    {
        if (!_mind.TryGetMind(user, out var mindId, out var mind))
            return false;

        if (mind.OwnedEntity is null)
            return false;

        _actions.GrantActions(user, storage.Comp.SpellEntities, storage.Owner);
        return true;
    }
}
