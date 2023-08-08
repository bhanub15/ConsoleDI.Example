namespace ConsoleDI.Example;

internal sealed class ExampleScopedService : IExampleScopedService
{
    static int counter = 0;
    private int _serialNum;
    public int SerialNum
    {
        get => _serialNum;
    }
    private void SetSerialNum()
    {
        _serialNum = counter++;
        Console.WriteLine($"ExampleScopedService: serialNum={_serialNum}");
    }
    Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    public ExampleScopedService()
    {
        SetSerialNum();
    }
}