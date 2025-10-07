using Content.Shared._CE14.MagicEnergy.Components;

namespace Content.Shared._CE14.MagicLantern;

public partial class CE14MagicLanternSystem : EntitySystem
{

    [Dependency] private readonly SharedPointLightSystem _pointLight = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CE14MagicLanternComponent, CE14SlotCrystalPowerChangedEvent>(OnSlotPowerChanged);
    }

    private void OnSlotPowerChanged(Entity<CE14MagicLanternComponent> ent, ref CE14SlotCrystalPowerChangedEvent args)
    {
        SharedPointLightComponent? pointLight = null;
        if (_pointLight.ResolveLight(ent, ref pointLight))
        {
            _pointLight.SetEnabled(ent, args.Powered, pointLight);
        }
    }
}
