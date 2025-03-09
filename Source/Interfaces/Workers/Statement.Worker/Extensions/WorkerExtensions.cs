using System.Reflection;

namespace Statement.Worker.Extensions;

public static class WorkerExtensions
{
    public static IHostBuilder AddBaseConfiguration(this IHostBuilder builder)
    {
        string environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "LOCAL";
        Configuration.Environment.SetEnvironmentTag(environment);

        RegionEndpoint region = Configuration.Environment.IsProductionEnvironment ? RegionEndpoint.SAEast1 : RegionEndpoint.USEast1;

        builder.ConfigureAppConfiguration((hostContext, config) =>
        {
            string currentPath = Assembly.GetExecutingAssembly().Location;
            config.AddJsonFile($"{currentPath}/appsettings.{Configuration.Environment.ExecutionEnvironment.ToLower()}.json", false, true);

            IConfigurationRoot configuration = config.Build();
            Configuration.Name = configuration.GetValue<string>("ApplicationName");
            configuration.GetSection("Azure").Bind(Configuration.Azure);
            configuration.GetSection("ConnectionStrings").Bind(Configuration.Database);
            configuration.GetSection("Logging").Bind(Configuration.Logging);
            configuration.GetSection("RabbitMq").Bind(Configuration.RabbitMqConfiguration);
            configuration.GetSection("Queues").Bind(Configuration.QueueAndExchange);
            configuration.GetSection("SecureGateway").Bind(Configuration.SecureGateway);

        });
        return builder;
    }

    public static IHostBuilder AddBaseServices(this IHostBuilder builder)
    {
        builder.UseSerilog();
        return builder;
    }
}
