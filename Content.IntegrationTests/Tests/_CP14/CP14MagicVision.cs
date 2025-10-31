using Content.Shared.StatusEffectNew;
using Robust.Shared.GameObjects;
using Robust.Shared.Prototypes;

namespace Content.IntegrationTests.Tests._CP14;

#nullable enable

[TestFixture]
public sealed class CP14MagicVision
{
    /// <summary>
    /// Check that the price of all resources to craft the item on the workbench is lower than the price of the result.
    /// </summary>
    [Test]
    public async Task CheckIfVisMaskIsGettingApplied()
    {
        await using var pair = await PoolManager.GetServerClient();
        var server = pair.Server;

        var entManager = server.ResolveDependency<IEntityManager>();
        var protoMan = server.ResolveDependency<IPrototypeManager>();

        var statusSystem = entManager.System<StatusEffectsSystem>();
        var target = entManager.Spawn();

        await server.WaitAssertion(() =>
        { });
    }
}
