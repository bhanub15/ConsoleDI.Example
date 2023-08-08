namespace ConsoleDI.Example;

internal sealed class ExampleScopedService : IExampleScopedService
{
    static int counter = 0;

    int IReportServiceLifetime.SerialNum { get; } = counter++;

    Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
}