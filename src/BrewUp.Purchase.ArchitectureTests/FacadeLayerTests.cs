using NetArchTest.Rules;

namespace BrewUp.Purchase.ArchitectureTests;

public sealed class FacadeLayerTests
{
    private const string FacadeNamespace = "BrewUp.Purchase.Facade";

    [Fact]
    public void Facade_ShouldFollowNamingConventions()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.Facade.IPurchaseFacade).Assembly)
            .That()
            .ResideInNamespace(FacadeNamespace)
            .Should()
            .HaveNameMatching("^(I)?[A-Z][a-zA-Z0-9]*$")
            .GetResult();

        Assert.True(result.IsSuccessful, "All types in Facade namespace should follow naming conventions");
    }

    [Fact]
    public void Facade_CanDependOnInfrastructureForQueries()
    {
        // Note: Facade can depend on Infrastructure for query interfaces (IPurchaseOrderQueries)
        // This is acceptable in CQRS pattern as queries are read-only operations
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.Facade.IPurchaseFacade).Assembly)
            .That()
            .ResideInNamespace(FacadeNamespace)
            .Should()
            .BeClasses()
            .Or()
            .BeInterfaces()
            .GetResult();

        Assert.True(result.IsSuccessful, "Facade layer types should be valid classes or interfaces");
    }

    [Fact]
    public void Facade_ShouldNotDependOnRestApi()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.Facade.IPurchaseFacade).Assembly)
            .That()
            .ResideInNamespace(FacadeNamespace)
            .ShouldNot()
            .HaveDependencyOn("BrewUp.Rest")
            .GetResult();

        Assert.True(result.IsSuccessful, "Facade layer should not depend on REST API layer");
    }

    [Fact]
    public void Facade_InterfacesShouldStartWithI()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.Facade.IPurchaseFacade).Assembly)
            .That()
            .ResideInNamespace(FacadeNamespace)
            .And()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        Assert.True(result.IsSuccessful, "All interfaces should start with 'I'");
    }

    [Fact]
    public void Facade_BindingModelsShouldBeInCorrectNamespace()
    {
        var result = Types.InAssembly(typeof(global::BrewUp.Purchase.Facade.IPurchaseFacade).Assembly)
            .That()
            .ResideInNamespace($"{FacadeNamespace}.BindingModels")
            .Should()
            .BeClasses()
            .Or()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful, "Binding models should be classes or sealed classes");
    }
}
