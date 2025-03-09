using Shared.Services.Contracts;
using Shared.UseCases;
using Statement.UseCases.Ingestion;
using Statement.UseCases.Ingestion.Contracts;

namespace Statement.Worker;

public class Worker(IRequestHandler<Request, BaseResponse<ResponseData>> handler,
    ILogger<Worker> logger, IQueue queue) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(() => queue.Consume(message =>
        {
            try
            {
                handler.Handle(new Request() { Message = message });

                if (stoppingToken.IsCancellationRequested)
                    queue.StopConsume();

                if (stoppingToken.IsCancellationRequested)
                    Task.Delay(600, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError
                (
                    exception: ex,
                    message: $"{Statement.Configuration.Name} receiver worker raised an error");
            }
        }), stoppingToken);
    }
}