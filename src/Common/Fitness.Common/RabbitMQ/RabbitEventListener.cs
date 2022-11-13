using Fitness.Common.EventStore.Events;
using Fitness.Common.Tools;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Fitness.Common.RabbitMQ;

public class RabbitEventListener : IRabbitEventListener
{
    private readonly string? _queueName;
    private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
    {
        ContractResolver = new PrivateResolver(),
        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        TypeNameHandling = TypeNameHandling.All
    };

    private const string ExchangeName = "fitness_event_bus";
    private readonly IRabbitBase _rabbitBase;
    private readonly IServiceScopeFactory _serviceFactory;
    private readonly ILoggerManager<RabbitEventListener> _loggerManager;

    public RabbitEventListener(IRabbitBase rabbitBase, IServiceScopeFactory serviceFactory,
        ILoggerManager<RabbitEventListener> loggerManager, string? queueName)
    {
        _queueName = queueName;
        _rabbitBase = rabbitBase ?? throw new ArgumentNullException(nameof(rabbitBase));
        _serviceFactory = serviceFactory;
        _loggerManager = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager));
    }

    public void Subscribe(Type type)
    {
        using var channel = _rabbitBase.GetOrCreateNewModelWhenItIsClosed();
        var args = _rabbitBase.CreateDeadLetterQueue(channel);

        var name = _queueName ?? AppDomain.CurrentDomain.FriendlyName.Trim().Trim('_') + "_" + type.Name;
        _rabbitBase.CreateConsumer(channel, ExchangeName, name, GetTypeName(type), args);

        var mainConsumer = new AsyncEventingBasicConsumer(channel);

        mainConsumer.Received += ConsumerMessageReceived;

        channel.BasicConsume(
            queue: name,
            autoAck: false,
            consumer: mainConsumer); 
    }

    public void Subscribe<TEvent>() where TEvent : IEvent
    {
        Subscribe(typeof(TEvent));
    }

    public Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        using var channel = _rabbitBase.GetOrCreateNewModelWhenItIsClosed();
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));

        _rabbitBase.CreatePublisher(channel, ExchangeName, GetTypeName(typeof(TEvent)), body);
        
        return Task.CompletedTask;
    }

    public void Publish(string message, string type)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentNullException(nameof(message), "Event message can not be null.");
        }

        if (string.IsNullOrWhiteSpace(type))
        {
            throw new ArgumentNullException(nameof(type), "Event type can not be null.");
        }

        using var channel = _rabbitBase.GetOrCreateNewModelWhenItIsClosed();

        _rabbitBase.CreatePublisher(channel, ExchangeName, type, Encoding.UTF8.GetBytes(message));
    }

    private async Task ConsumerMessageReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        _loggerManager.LogInformation($"Received new message: {message}");

        var data = JsonConvert.DeserializeObject<IEvent>(message, _settings);

        using var scope = _serviceFactory.CreateScope();
        var eventBus = scope.ServiceProvider.GetService<IEventBus>();
        if (data != null)
        {
            await eventBus?.PublishLocalAsync(data)!;
        }
    } 
     
    private string GetTypeName(Type type)
    {
        var name = type.FullName?.ToLower().Replace("+", ".");

        if (type is IEvent)
        {
            name += "_event";
        }

        return name;
    }
}