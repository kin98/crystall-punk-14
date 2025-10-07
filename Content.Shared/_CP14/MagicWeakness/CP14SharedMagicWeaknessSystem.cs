using Content.Shared._CE14.MagicEnergy;
using Content.Shared.Bed.Sleep;
using Content.Shared.Damage;
using Content.Shared.IdentityManagement;
using Content.Shared.Popups;
using Content.Shared.StatusEffectNew;
using Content.Shared.Trigger.Systems;

namespace Content.Shared._CE14.MagicWeakness;

public abstract class CE14SharedMagicWeaknessSystem : EntitySystem
{
    [Dependency] private readonly StatusEffectsSystem _statusEffects = default!;
    [Dependency] private readonly DamageableSystem _damageable = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly TriggerSystem _trigger = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14MagicUnsafeDamageComponent, CE14MagicEnergyBurnOutEvent>(OnMagicEnergyBurnOutDamage);
        SubscribeLocalEvent<CE14MagicUnsafeDamageComponent, CE14MagicEnergyOverloadEvent>(OnMagicEnergyOverloadDamage);

        SubscribeLocalEvent<CE14MagicUnsafeSleepComponent, CE14MagicEnergyBurnOutEvent>(OnMagicEnergyBurnOutSleep);
        SubscribeLocalEvent<CE14MagicUnsafeSleepComponent, CE14MagicEnergyOverloadEvent>(OnMagicEnergyOverloadSleep);

        SubscribeLocalEvent<CE14MagicUnsafeTriggerComponent, CE14MagicEnergyBurnOutEvent>(OnMagicEnergyBurnOutTrigger);
        SubscribeLocalEvent<CE14MagicUnsafeTriggerComponent, CE14MagicEnergyOverloadEvent>(OnMagicEnergyOverloadTrigger);
    }

    private void OnMagicEnergyBurnOutSleep(Entity<CE14MagicUnsafeSleepComponent> ent,
        ref CE14MagicEnergyBurnOutEvent args)
    {
        if (args.BurnOutEnergy > ent.Comp.SleepThreshold)
        {
            _popup.PopupEntity(Loc.GetString("CE14-magic-energy-damage-burn-out-fall"),
                ent,
                ent,
                PopupType.LargeCaution);
            _statusEffects.TryAddStatusEffectDuration(
                ent,
                SleepingSystem.StatusEffectForcedSleeping,
                TimeSpan.FromSeconds(ent.Comp.SleepPerEnergy * (float)args.BurnOutEnergy));
        }
    }

    private void OnMagicEnergyOverloadSleep(Entity<CE14MagicUnsafeSleepComponent> ent,
        ref CE14MagicEnergyOverloadEvent args)
    {
        if (args.OverloadEnergy > ent.Comp.SleepThreshold)
        {
            _popup.PopupEntity(Loc.GetString("CE14-magic-energy-damage-burn-out-fall"),
                ent,
                ent,
                PopupType.LargeCaution);
            _statusEffects.TryAddStatusEffectDuration(
                ent,
                SleepingSystem.StatusEffectForcedSleeping,
                TimeSpan.FromSeconds(ent.Comp.SleepPerEnergy * (float)args.OverloadEnergy));
        }
    }

    private void OnMagicEnergyOverloadTrigger(Entity<CE14MagicUnsafeTriggerComponent> ent, ref CE14MagicEnergyOverloadEvent args)
    {
        _trigger.Trigger(ent);
    }

    private void OnMagicEnergyBurnOutTrigger(Entity<CE14MagicUnsafeTriggerComponent> ent, ref CE14MagicEnergyBurnOutEvent args)
    {
        _trigger.Trigger(ent);
    }

    private void OnMagicEnergyBurnOutDamage(Entity<CE14MagicUnsafeDamageComponent> ent,
        ref CE14MagicEnergyBurnOutEvent args)
    {
        //TODO: Idk why this dont popup recipient
        //Others popup
        _popup.PopupPredicted(Loc.GetString("CE14-magic-energy-damage-burn-out"),
            Loc.GetString("CE14-magic-energy-damage-burn-out-other", ("name", Identity.Name(ent, EntityManager))),
            ent,
            ent);

        //Local self popup
        _popup.PopupEntity(
            Loc.GetString("CE14-magic-energy-damage-burn-out"),
            ent,
            ent,
            PopupType.LargeCaution);

        _damageable.TryChangeDamage(ent, ent.Comp.DamagePerEnergy * args.BurnOutEnergy, interruptsDoAfters: false);
    }

    private void OnMagicEnergyOverloadDamage(Entity<CE14MagicUnsafeDamageComponent> ent,
        ref CE14MagicEnergyOverloadEvent args)
    {
        //TODO: Idk why this dont popup recipient
        //Others popup
        _popup.PopupPredicted(Loc.GetString("CE14-magic-energy-damage-overload"),
            Loc.GetString("CE14-magic-energy-damage-overload-other", ("name", Identity.Name(ent, EntityManager))),
            ent,
            ent);

        //Local self popup
        _popup.PopupEntity(
            Loc.GetString("CE14-magic-energy-damage-overload"),
            ent,
            ent,
            PopupType.LargeCaution);

        _damageable.TryChangeDamage(ent, ent.Comp.DamagePerEnergy * args.OverloadEnergy, interruptsDoAfters: false);
    }
}
