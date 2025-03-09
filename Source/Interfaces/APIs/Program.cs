using Asp.Versioning;
using StatementAssets.Endpoints;
using StatementAssets.Extensions;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
    options.AssumeDefaultVersionWhenUnspecified = true;
})
.AddMvc()
.AddApiExplorer(options =>
 {
     options.GroupNameFormat = "'v'V";
     options.SubstituteApiVersionInUrl = true;
 });

builder.AddBaseServices();
builder.AddBaseConfiguration(); 

StatementAssets.Context.ConfigureServices(builder.Services);


// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("LocalCorsPolicy");

app.UseAuthorization();

app.MapControllers();
app.MapStatementEndpoints();
    

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
});

app.Run();