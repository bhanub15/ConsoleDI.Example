namespace ConsoleDI.Example;

internal sealed class ExampleSingletonService : IExampleSingletonService
{
    static int counter = 0;

    int IReportServiceLifetime.SerialNum { get; } = counter++;

    Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
}