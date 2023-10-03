using System.Reflection;


namespace PokemonPvpRanker.Extensions;

public static class IServiceCollectionExtensions
{
    public static void RegisterClasses(this IServiceCollection services)
    {
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => typeof(ISingleton).IsAssignableFrom(x) && !x.IsAbstract)
            .ToList()
            .ForEach(x => services.AddSingleton(x));

        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => typeof(IScoped).IsAssignableFrom(x) && !x.IsAbstract)
            .ToList()
            .ForEach(x => services.AddScoped(x));

        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => typeof(ITransient).IsAssignableFrom(x) && !x.IsAbstract)
            .ToList()
            .ForEach(x => services.AddTransient(x));
    }
}

public interface ISingleton { }
public interface IScoped { }
public interface ITransient { }