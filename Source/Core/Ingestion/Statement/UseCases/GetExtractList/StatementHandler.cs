namespace Statement.UseCases.GetExtractList;

public class StatementHandler : IRequestHandlers<StatementRequest, BaseResponse<StatementResponse>>
{
    private readonly IStatementRepository _statementRepository;

    public StatementHandler(IStatementRepository repository)
    {
        _statementRepository = repository;
    }

    public BaseResponse<StatementResponse> Handle(StatementRequest request)
    {
        var investments = _statementRepository.GetInvestments(request);

        var response = new StatementResponse
        {
            Investments = investments.Select(i => new InvestmentResponse
            {
                ProductName = i.ProductName,
                ProductCode = i.ProductCode,
                InvestedBalance = i.InvestedBalance
            }).ToList()
        };

        return new BaseResponse<StatementResponse>
        {
            Data = response,
            HttpStatusCode = 200,
            Message = "Investments retrieved successfully"
        };
    }
}