namespace RabbitMQ;

public class ConsumerQueue : IQueue
{

    public ConsumerQueue(ILogger<ConsumerQueue> logger)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        var connection = factory.CreateConnectionAsync();

    }

    public void Consume(Action<string> action)
    {

    }

    public bool Produce(string message)
    {
        return true;
    }

    public void StopConsume()
    {
    }
}
