using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDI.Example;

public interface IReportServiceLifetime
{
    int SerialNum {
        get;
    }
    Guid Id { get; }

    ServiceLifetime Lifetime { get; }
}