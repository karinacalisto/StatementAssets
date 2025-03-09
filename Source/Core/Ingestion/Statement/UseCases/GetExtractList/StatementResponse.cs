using Shared.UseCases.Contracts;

namespace Statement.UseCases.GetExtractList;

public class StatementResponse : IReponseData
{
    public List<InvestmentResponse> Investments { get; set; }
}
