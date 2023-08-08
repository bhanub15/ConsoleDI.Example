namespace ConsoleDI.Example;

internal sealed class ExampleTransientService : IExampleTransientService
{
    static int counter = 0;
    private int _serialNum;

    int IReportServiceLifetime.SerialNum
    {
        get => _serialNum;
    }

    private void SetSerialNum()
    {
        _serialNum = counter++;
        Console.WriteLine($"ExampleTransientService: serialNum={_serialNum}");
    }
    Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();

    public ExampleTransientService()
    {
        SetSerialNum();
    }
}