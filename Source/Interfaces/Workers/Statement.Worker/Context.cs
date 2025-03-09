using RabbitMQ;
using Shared.UseCases;
using Statement.Data.DbContexts.Context;
using Statement.Data.DbContexts.UseCases.Persist;
using Statement.UseCases.Ingestion;
using Statement.UseCases.Ingestion.Contracts;
using System.Reflection.Metadata;

namespace Statement.Worker;

public class Context
{
    public static void ConfigureServices(IServiceCollection services, HostBuilderContext hostContext)
    {
        services.AddHttpClient();
        services.AddSerilog();

        services.AddTransient<IRepository, Repository>();
        services.AddTransient<Shared.Services.Contracts.IQueue, ConsumerQueue>();
        // services.AddTransient<IRequestHandler<Request, BaseResponse<ResponseData>>, Handler>();

        services.AddDbContext<StatementDbContext>(ServiceLifetime.Singleton);

        services.AddHostedService<Statement.Worker.Worker>();
    }
}
