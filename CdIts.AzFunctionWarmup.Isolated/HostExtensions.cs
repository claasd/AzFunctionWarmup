using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CdIts.AzFunctionWarmup;

public static class HostExtensions
{
    public static async Task WarmupAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        await FunctionWarmup.WarmUpAsync(scope);
    }
    public static void Warmup(this IHost host)
    {
        WarmupAsync(host).Wait();
    }
    
}
