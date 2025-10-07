using Content.Client.Items;
using Content.Shared._CE14.MagicEnergy;
using Content.Shared._CE14.MagicEnergy.Components;

namespace Content.Client._CE14.MagicEnergy;

public sealed class CE14MagicEnergySystem : CE14SharedMagicEnergySystem
{
    public override void Initialize()
    {
        base.Initialize();

        Subs.ItemStatus<CE14MagicEnergyExaminableComponent>( ent => new CE14MagicEnergyStatusControl(ent));
    }
}
