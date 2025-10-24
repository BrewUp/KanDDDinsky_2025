using NetArchTest.Rules;

namespace BrewUp.Purchase.ArchitectureTests;

public sealed class DomainLayerTests
{
    private const string DomainNamespace = "BrewUp.Purchase.Domain";

    [Fact]
    public void Domain_ShouldNotDependOnInfrastructure()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.Domain.Commands.Handlers.CreatePurchaseOrderHandler).Assembly)
            .That()
            .ResideInNamespace(DomainNamespace)
            .ShouldNot()
            .HaveDependencyOn("BrewUp.Purchase.Infrastructure")
            .GetResult();

        Assert.True(result.IsSuccessful, "Domain layer should not depend on Infrastructure layer");
    }

    [Fact]
    public void Domain_ShouldNotDependOnFacade()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.Domain.Commands.Handlers.CreatePurchaseOrderHandler).Assembly)
            .That()
            .ResideInNamespace(DomainNamespace)
            .ShouldNot()
            .HaveDependencyOn("BrewUp.Purchase.Facade")
            .GetResult();

        Assert.True(result.IsSuccessful, "Domain layer should not depend on Facade layer");
    }

    [Fact]
    public void Domain_ShouldNotDependOnReadModel()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.Domain.Commands.Handlers.CreatePurchaseOrderHandler).Assembly)
            .That()
            .ResideInNamespace(DomainNamespace)
            .ShouldNot()
            .HaveDependencyOn("BrewUp.Purchase.ReadModel")
            .GetResult();

        Assert.True(result.IsSuccessful, "Domain layer should not depend on ReadModel layer");
    }

    [Fact]
    public void Domain_CommandHandlersShouldBeSealed()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.Domain.Commands.Handlers.CreatePurchaseOrderHandler).Assembly)
            .That()
            .ResideInNamespace($"{DomainNamespace}.Commands.Handlers")
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful, "All command handlers should be sealed classes");
    }

    [Fact]
    public void Domain_AggregatesShouldBeInEntitiesNamespace()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.Domain.Commands.Handlers.CreatePurchaseOrderHandler).Assembly)
            .That()
            .ResideInNamespace($"{DomainNamespace}.Entities")
            .Should()
            .BeClasses()
            .GetResult();

        Assert.True(result.IsSuccessful, "All aggregates should be classes in Entities namespace");
    }
}
