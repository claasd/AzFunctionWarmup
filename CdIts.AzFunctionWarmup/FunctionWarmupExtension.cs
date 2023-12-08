using CdIts.AzFunctionWarmup;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(FunctionWarmupService), "FunctionWarmupService")]

namespace CdIts.AzFunctionWarmup;

[Extension("ServiceWarmup")]
public class FunctionWarmupExtension : IExtensionConfigProvider
{
    private readonly IServiceScopeFactory _scopeFactory;

    public FunctionWarmupExtension(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void Initialize(ExtensionConfigContext context)
    {
        if (FunctionWarmup.OrderedWarmups.Any() && !FunctionWarmup.OrderedWarmups.Any())
            return;
        using var scope = _scopeFactory.CreateScope();
        FunctionWarmup.WarmUpAsync(scope).Wait();
    }
}