namespace ConsoleDI.Example;

internal sealed class ExampleSingletonService : IExampleSingletonService
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
        Console.WriteLine($"ExampleSingletonService: serialNum={_serialNum}");
    }
    Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    public ExampleSingletonService()
    {
        SetSerialNum();
    }
}