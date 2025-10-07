using Content.Shared._CE14.Explosion.Components;
using Content.Shared.Damage;

namespace Content.Shared.Trigger.Systems;

public sealed partial class TriggerSystem
{
    private void InitializeDamageReceived()
    {
        SubscribeLocalEvent<CE14TriggerOnDamageReceivedComponent, DamageChangedEvent>(OnDamageReceived);
    }

    private void OnDamageReceived(EntityUid uid, CE14TriggerOnDamageReceivedComponent component, DamageChangedEvent args)
    {
        if (!args.DamageIncreased)
            return;

        Trigger(uid);
    }
}
