using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CdIts.AzFunctionWarmup;

public class FunctionWarmup
{
    public FunctionWarmup()
    {
    }
    public static List<Type> ParallelWarmups { get; } = new();
    public static List<Type> OrderedWarmups { get; } = new();

    public static async Task WarmUpAsync(IServiceScope scope)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<FunctionWarmup>>();
        logger.LogInformation("Beginning Service warmup");
        foreach (var warmup in OrderedWarmups)
        {
            await Warmup(warmup, scope.ServiceProvider, logger);
        }

        await Task.WhenAll(ParallelWarmups.Distinct().Select(warmup => Warmup(warmup, scope.ServiceProvider, logger)).ToArray());
        logger.LogInformation("Finished Service warmup");
    }
    private static async Task Warmup(Type warmup, IServiceProvider services, ILogger logger)
    {
        logger.LogInformation("Warming up {Type}", warmup.Name);
        var service = services.GetService(warmup);
        var warmupService = service as IWarmup;
        await warmupService!.WarmupAsync();
        logger.LogInformation("Finished Warming up {Type}", warmup.Name);
    }
}