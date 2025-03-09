namespace Statement.UseCases.GetExtractList.Contracts;

public interface IRequestHandlers<in TRequest, TResponse> where TRequest
    : IRequest<TResponse>
{
    public TResponse Handle(TRequest request);
}