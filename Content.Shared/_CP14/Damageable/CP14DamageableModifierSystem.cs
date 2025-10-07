using Content.Shared.Damage;

namespace Content.Shared._CE14.Damageable;

public sealed class CE14DamageableModifierSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14DamageableModifierComponent, DamageModifyEvent>(OnDamageModify);
    }

    private void OnDamageModify(Entity<CE14DamageableModifierComponent> ent, ref DamageModifyEvent args)
    {
        args.Damage *= ent.Comp.Modifier;
    }
}
