namespace StatementAssets;

public static class Context
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddHttpContextAccessor();

        services.AddHttpClient();
        services.AddSerilog();

        services.AddTransient<IStatementRepository, StatementRepository>();
        services.AddTransient<Statement.UseCases.GetExtractList.Contracts.IRequestHandlers<StatementRequest, BaseResponse<StatementResponse>>, StatementHandler>();
        
        services.AddDbContext<StatementDbContext>(ServiceLifetime.Scoped);
        
        services.AddCors(options =>
        {
            options.AddPolicy("LocalCorsPolicy", builder =>
            {
                builder.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });
        });
    }
}