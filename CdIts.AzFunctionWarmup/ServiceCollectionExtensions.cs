using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace CdIts.AzFunctionWarmup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSingletonWithWarmup<TService,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TImplementation>(this IServiceCollection services, bool parallelWarmup = true)
        where TImplementation : class, IWarmup, TService where TService : class
    {
        if (parallelWarmup)
            FunctionWarmupExtension.ParallelWarmups.Add(typeof(TService));
        else
            FunctionWarmupExtension.OrderedWarmups.Add(typeof(TService));
        return services.AddSingleton<TService, TImplementation>();
    }
}
