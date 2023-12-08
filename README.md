# AzFunctionWarmup
Extension that adds a AddSingletonWithWarmup method to IServiceCollection. Instances need to implement IWarmup and define a WarmupAsync method. The method is called before azure functions are discovered, but after all dependencies are registered. Supports dependency injection for warmup classes.

## isolated worker model
For isolated worker model, use the `CdIts.AtFunctionWarmup.Isolated` package following code in your host startup before running the app:
```csharp
await host.WarmupAsync();
```
## License
MIT License
