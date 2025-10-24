using NetArchTest.Rules;

namespace BrewUp.Purchase.ArchitectureTests;

public sealed class ReadModelLayerTests
{
    private const string ReadModelNamespace = "BrewUp.Purchase.ReadModel";

    [Fact]
    public void ReadModel_ShouldNotDependOnDomain()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.ReadModel.Events.Handlers.PurchaseOrderCreatedHandler).Assembly)
            .That()
            .ResideInNamespace(ReadModelNamespace)
            .ShouldNot()
            .HaveDependencyOn("BrewUp.Purchase.Domain")
            .GetResult();

        Assert.True(result.IsSuccessful, "ReadModel layer should not depend on Domain layer");
    }

    [Fact]
    public void ReadModel_ShouldNotDependOnFacade()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.ReadModel.Events.Handlers.PurchaseOrderCreatedHandler).Assembly)
            .That()
            .ResideInNamespace(ReadModelNamespace)
            .ShouldNot()
            .HaveDependencyOn("BrewUp.Purchase.Facade")
            .GetResult();

        Assert.True(result.IsSuccessful, "ReadModel layer should not depend on Facade layer");
    }

    [Fact]
    public void ReadModel_EventHandlersShouldBeSealed()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.ReadModel.Events.Handlers.PurchaseOrderCreatedHandler).Assembly)
            .That()
            .ResideInNamespace($"{ReadModelNamespace}.Events.Handlers")
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful, "All event handlers should be sealed classes");
    }

    [Fact]
    public void ReadModel_ServicesShouldBeInServicesNamespace()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.ReadModel.Events.Handlers.PurchaseOrderCreatedHandler).Assembly)
            .That()
            .ResideInNamespace($"{ReadModelNamespace}.Services")
            .Should()
            .BeClasses()
            .Or()
            .BeInterfaces()
            .GetResult();

        Assert.True(result.IsSuccessful, "All services should be classes or interfaces in Services namespace");
    }
}
