using Robust.Shared.GameObjects;
using Robust.Shared.Prototypes;

namespace Content.IntegrationTests.Tests._CE14;

#nullable enable

[TestFixture]
public sealed class CE14EntityTest
{
    [Test]
    public async Task CheckAllCE14EntityHasForkFilteredCategory()
    {
        await using var pair = await PoolManager.GetServerClient();
        var server = pair.Server;

        var compFactory = server.ResolveDependency<IComponentFactory>();
        var protoManager = server.ResolveDependency<IPrototypeManager>();

        await server.WaitAssertion(() =>
        {
            Assert.Multiple(() =>
            {
                if (!protoManager.TryIndex<EntityCategoryPrototype>("ForkFiltered", out var indexedFilter))
                    return;

                foreach (var proto in protoManager.EnumeratePrototypes<EntityPrototype>())
                {
                    if (!proto.ID.StartsWith("CE14"))
                        continue;

                    if (proto.Abstract || proto.HideSpawnMenu)
                        continue;

                    Assert.That(proto.Categories.Contains(indexedFilter), $"CE14 fork proto: {proto} does not marked abstract, or have a HideSpawnMenu or ForkFiltered category");
                }
            });
        });
        await pair.CleanReturnAsync();
    }
}
