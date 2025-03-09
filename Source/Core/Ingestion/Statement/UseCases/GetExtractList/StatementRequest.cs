using Shared.UseCases;
using Shared.UseCases.Contracts;

namespace Statement.UseCases.GetExtractList;

public class StatementRequest : IRequest<BaseResponse<StatementResponse>>
{
    public string ProductName { get; set; } = string.Empty;
    public string Agency { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public int DAC { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}