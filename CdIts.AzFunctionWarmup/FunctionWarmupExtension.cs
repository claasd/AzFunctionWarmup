using CdIts.AzFunctionWarmup;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: WebJobsStartup(typeof(FunctionWarmupService), "FunctionWarmupService")]

namespace CdIts.AzFunctionWarmup;

[Extension("ServiceWarmup")]
public class FunctionWarmupExtension : IExtensionConfigProvider
{
    private readonly IServiceScopeFactory _scopeFactory;
    internal static readonly List<Type> ParallelWarmups = new();
    internal static readonly List<Type> OrderedWarmups = new();

    public FunctionWarmupExtension(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void Initialize(ExtensionConfigContext context)
    {
        if (!OrderedWarmups.Any() && !ParallelWarmups.Any())
            return;
        using var scope = _scopeFactory.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<FunctionWarmupExtension>>();
        logger.LogInformation("Beginning Service warmup");
        foreach (var warmup in OrderedWarmups)
        {
            Warmup(warmup, scope.ServiceProvider, logger).Wait();
        }

        Task.WaitAll(ParallelWarmups.Distinct().Select(warmup => Warmup(warmup, scope.ServiceProvider, logger))
            .ToArray());
        logger.LogInformation("Finished Service warmup");
    }

    private async Task Warmup(Type warmup, IServiceProvider services, ILogger logger)
    {
        logger.LogInformation("Warming up {Type}", warmup.Name);
        var service = services.GetService(warmup);
        var warmupService = service as IWarmup;
        await warmupService!.WarmupAsync();
        logger.LogInformation("Finished Warming up {Type}", warmup.Name);
    }
}