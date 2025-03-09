namespace Statement.UseCases.Ingestion.Contracts;

public interface IRequestHandler<in TRequest, TResponse> where TRequest 
    : IRequest<TResponse>
{
    public void Handle(TRequest request);
}
