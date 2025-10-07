using Robust.Server.GameStates;

namespace Content.Server._CE14.PVS;

public sealed partial class CE14PvsOverrideSystem : EntitySystem
{
    [Dependency] private readonly PvsOverrideSystem _pvs = default!;
    public override void Initialize()
    {
        SubscribeLocalEvent<CE14PvsOverrideComponent, ComponentStartup>(OnLighthouseStartup);
        SubscribeLocalEvent<CE14PvsOverrideComponent, ComponentShutdown>(OnLighthouseShutdown);
    }

    private void OnLighthouseShutdown(Entity<CE14PvsOverrideComponent> ent, ref ComponentShutdown args)
    {
        _pvs.RemoveGlobalOverride(ent);
    }

    private void OnLighthouseStartup(Entity<CE14PvsOverrideComponent> ent, ref ComponentStartup args)
    {
        _pvs.AddGlobalOverride(ent);
    }
}
