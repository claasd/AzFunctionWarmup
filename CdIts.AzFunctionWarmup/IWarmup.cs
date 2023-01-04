namespace CdIts.AzFunctionWarmup;

public interface IWarmup
{
    /// <summary>
    /// This warmup task will be called prior to the function host starting
    /// </summary>
    Task WarmupAsync();
}