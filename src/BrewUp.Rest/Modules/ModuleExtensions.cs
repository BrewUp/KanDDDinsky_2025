using System.Reflection;

namespace BrewUp.Rest.Modules;

public static class ModuleExtensions
{
    private static readonly List<IModule> RegisteredModules = new();

    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
    {
        var modules = DiscoverModules();

        foreach (var module in modules.OrderBy(m => m.Order))
        {
            if (!module.IsEnabled)
                continue;

            ValidateDependencies(module, modules);

            module.Register(builder);
            RegisteredModules.Add(module);
        }

        return builder;
    }

    public static WebApplication ConfigureModules(this WebApplication app)
    {
        foreach (var module in RegisteredModules.OrderBy(m => m.Order))
        {
            module.Configure(app);
        }

        return app;
    }

    private static List<IModule> DiscoverModules()
    {
        var moduleType = typeof(IModule);
        var modules = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => moduleType.IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .Select(t => (IModule)Activator.CreateInstance(t)!)
            .ToList();

        return modules;
    }

    private static void ValidateDependencies(IModule module, List<IModule> allModules)
    {
        foreach (var dependency in module.DependsOn)
        {
            var dependencyExists = allModules.Any(m =>
                m.GetType() == dependency.GetType() && m.IsEnabled);

            if (!dependencyExists)
            {
                throw new InvalidOperationException(
                    $"Module {module.GetType().Name} depends on {dependency.GetType().Name}, but it is not enabled or registered.");
            }
        }
    }
}
