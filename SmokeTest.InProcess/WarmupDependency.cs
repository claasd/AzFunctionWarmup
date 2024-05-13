using System.Threading.Tasks;
using CdIts.AzFunctionWarmup;

namespace SmokeTest.InProcess;

public class WarmupDependency : IWarmupDependency, IWarmup
{
    private string _name = "<invalid>";
    public string Name() => _name;

    public Task WarmupAsync()
    {
        _name = "WarmupDependency";
        return Task.CompletedTask;
    }
}