using Shared.UseCases;
using Shared.UseCases.Contracts;
using Statement.UseCases.GetExtractList;

namespace Statement.UseCases.Ingestion;

public class Request : IRequest<BaseResponse<ResponseData>>
{
    public string? Message { get; set; }
}
