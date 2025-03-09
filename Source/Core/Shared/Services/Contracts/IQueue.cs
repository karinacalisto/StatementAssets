namespace Shared.Services.Contracts;

public interface IQueue
{
    void Consume (Action<string> action);
    bool Produce(string message);
    void StopConsume();
}