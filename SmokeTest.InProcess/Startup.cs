using CdIts.AzFunctionWarmup;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmokeTest.InProcess;

[assembly: FunctionsStartup(typeof(Startup))]

namespace SmokeTest.InProcess;

public class Startup : FunctionsStartup
{

    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingletonWithWarmup<IWarmupDependency, WarmupDependency>();
    }
}