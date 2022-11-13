using RabbitMQ.Client;

namespace Fitness.Common.RabbitMQ;

public interface IRabbitBase : IDisposable
{
    IModel GetOrCreateNewModelWhenItIsClosed();
    Dictionary<string, object> CreateDeadLetterQueue(IModel model);
    void CreatePublisher(IModel model, string exchangeName, string routingKey, byte[] body); 
    void CreateConsumer(IModel model, string exchangeName, string queueName, string routingKey, Dictionary<string, object> useArgs);
}