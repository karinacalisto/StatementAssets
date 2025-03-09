using Statement.Worker;

var builder = Host.CreateDefaultBuilder(args);

builder.AddBaseConfiguration();
builder.AddBaseServices();

builder.UseSerilog();
builder.ConfigureServices((hostContext, services) => Context.ConfigureServices(services, hostContext));

var host = builder.Build(); 
await host.RunAsync();