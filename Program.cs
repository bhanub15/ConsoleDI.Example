using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConsoleDI.Example;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<IExampleTransientService, ExampleTransientService>();
builder.Services.AddScoped<IExampleScopedService, ExampleScopedService>();
builder.Services.AddSingleton<IExampleSingletonService, ExampleSingletonService>();
builder.Services.AddTransient<ServiceLifetimeReporter>();

using IHost host = builder.Build();

ExemplifyServiceLifetime(host.Services, "Lifetime 1");
ExemplifyServiceLifetime(host.Services, "Lifetime 2");

await host.RunAsync();

static void ExemplifyServiceLifetime(IServiceProvider hostProvider, string lifetime)
{
    Console.WriteLine("Using hostProvider to resolve services..");
    ServiceLifetimeReporter logger0 = hostProvider.GetRequiredService<ServiceLifetimeReporter>();
    logger0.ReportServiceLifetimeDetails(
        $"{lifetime}: Call 0.1 to hostProvider.GetRequiredService<ServiceLifetimeReporter>()");

    Console.WriteLine("Using hostProvider to resolve services..");
    logger0 = hostProvider.GetRequiredService<ServiceLifetimeReporter>();
    logger0.ReportServiceLifetimeDetails(
        $"{lifetime}: Call 0.2 to hostProvider.GetRequiredService<ServiceLifetimeReporter>()");

    Console.WriteLine("Will be creating a scope now..");

    //My learnings/inferences notes:

    //Before this line, 
    //  no scope was created.
    //  'hostProvider' will be the root container or serviceprovider.
    //  'hostProvider' will be used to resolve services, even the scoped one.
    //  So the scoped resource actually has the lifespan like a singleton.

    //After this line, 
    //  a scope is created and the scope's serviceProvider is used to resolve ServiceLifetimeReporter.
    //  the scoped dependency in ServiceLifetimeReporter will have lifespan of the scope.

    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    Console.WriteLine("Using scope's container to resolve services..");
    ServiceLifetimeReporter logger = provider.GetRequiredService<ServiceLifetimeReporter>();
    logger.ReportServiceLifetimeDetails(
        $"{lifetime}: Call 1 to provider.GetRequiredService<ServiceLifetimeReporter>()");

    Console.WriteLine("...");

    Console.WriteLine("Using scope's container to resolve services..");
    logger = provider.GetRequiredService<ServiceLifetimeReporter>();
    logger.ReportServiceLifetimeDetails(
        $"{lifetime}: Call 2 to provider.GetRequiredService<ServiceLifetimeReporter>()");

    Console.WriteLine();
}