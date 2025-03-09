using System.Text.Json;

namespace Statement.UseCases.Ingestion;

public class Handler : IRequestHandler<Request, BaseResponse<ResponseData>>
{
    private readonly Request _request;
    private readonly ILogger<Handler> _logger;
    private readonly IRepository _repository;

    public void Handle(Request request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Message))
            {
                _logger.LogWarning("[DISCARTED] Invalid Message");
                return;
            }

            var ingestion = JsonSerializer.Deserialize<Investments>(request.Message);

            _repository.SaveAccount(ingestion.Account);
            _repository.SaveCashComposition(ingestion.CashComposition);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while handling the request");
            throw;
        }
    }
}
