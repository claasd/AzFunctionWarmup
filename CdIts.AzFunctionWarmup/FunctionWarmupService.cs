using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

namespace CdIts.AzFunctionWarmup;

public class FunctionWarmupService : IWebJobsStartup
{
    public void Configure(IWebJobsBuilder builder) => builder.AddExtension<FunctionWarmupExtension>();
}