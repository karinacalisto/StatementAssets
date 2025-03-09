namespace StatementAssets.Extensions;

public static class Extensions
{
    public static WebApplicationBuilder AddBaseConfiguration(this WebApplicationBuilder builder)
    {
        string environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        Configuration.Environment.SetEnvironmentTag(environment);

        builder.Configuration.AddJsonFile($"appsettings.{Configuration.Environment.ExecutionEnvironment.ToLower()}.json", optional: true, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Configuration;

        Configuration.Database.Statement = configuration.GetConnectionString("Statement");

        if (string.IsNullOrEmpty(Configuration.Database.Statement))
        {
            throw new InvalidOperationException("The connection string for Statement is not properly configured.");
        }

        return builder;
    }

    public static void AddBaseServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Statement Assets Consumer API",
                Description = "API for Statement"
            });
        });
        builder.Services.AddEndpointsApiExplorer();

    }
}