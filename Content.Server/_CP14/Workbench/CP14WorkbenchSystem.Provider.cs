using Content.Shared.Placeable;

namespace Content.Server._CE14.Workbench;

public sealed partial class CE14WorkbenchSystem
{
    private void InitProviders()
    {
        SubscribeLocalEvent<CE14WorkbenchPlaceableProviderComponent, CE14WorkbenchGetResourcesEvent>(OnGetResource);
    }

    private void OnGetResource(Entity<CE14WorkbenchPlaceableProviderComponent> ent, ref CE14WorkbenchGetResourcesEvent args)
    {
        if (!TryComp<ItemPlacerComponent>(ent, out var placer))
            return;

        args.AddResources(placer.PlacedEntities);
    }
}

public sealed class CE14WorkbenchGetResourcesEvent : EntityEventArgs
{
    public HashSet<EntityUid> Resources { get; private set; } = new();

    public void AddResource(EntityUid resource)
    {
        Resources.Add(resource);
    }

    public void AddResources(IEnumerable<EntityUid> resources)
    {
        foreach (var resource in resources)
        {
            Resources.Add(resource);
        }
    }
}
