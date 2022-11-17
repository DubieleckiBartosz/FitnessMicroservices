using Fitness.Common.Polly; 
using Microsoft.Extensions.Options;
using RabbitMQ.Client; 

namespace Fitness.Common.RabbitMQ;

public class RabbitBase : IRabbitBase
{ 
    private readonly Policy _policy;
    private const string ExchangeName = "dead_exchange"; 
    private const string QueueName = "dead_queue"; 

    private new readonly Dictionary<string, object> _args = new()
    {
        {"x-dead-letter-exchange", ExchangeName},
        {"x-message-ttl", 30000} ,
        {"x-max-length", 10000}
    };

    private readonly ILoggerManager<RabbitBase> _logger;
    protected IModel? Channel { get; private set; }
    private IConnection? _connection;
    private readonly ConnectionFactory _connectionFactory; 

    public RabbitBase(IOptions<RabbitOptions> options, ILoggerManager<RabbitBase> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        var rabbitOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _connectionFactory = new ConnectionFactory
        {
            HostName = rabbitOptions.Host,
            Port = rabbitOptions.Port,
            Password = rabbitOptions.Password,
            UserName = rabbitOptions.User 
        };

        var policySetup = new PolicySetup();
        _policy = policySetup.PolicyBrokerConnection(logger);
    } 

    public IModel GetOrCreateNewModelWhenItIsClosed()
    {
        if (Channel is {IsOpen: true})
        {
            return Channel;
        }

        if (_connection == null || _connection.IsOpen == false)
        {
            _policy.Execute(() =>
            {
                _connection = _connectionFactory.CreateConnection();
            }); 
        }

        if (_connection is { IsOpen: true })
        {
            Channel = _connection.CreateModel();
            return Channel;
        }
        else
        {
            _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");
            throw new ArgumentNullException($"{Channel} cannot be null");
        } 
    }

    public void CreateConsumer(IModel model, string exchangeName, string queueName, string routingKey,
        Dictionary<string, object> useArgs)
    {
        model.ExchangeDeclare(
            exchangeName,
            ExchangeType.Direct,
            true);

        model.QueueDeclare(
            queueName,
            false,
            false,
            false,
            useArgs);

        model.QueueBind(
            queueName,
            exchangeName,
            routingKey); 
    }

    public void CreatePublisher(IModel model, string exchangeName, string routingKey, byte[] body)
    {
        model.ExchangeDeclare(
            exchangeName,
            ExchangeType.Direct,
            true,
            false);

        _policy.Execute(() =>
        {
            model.BasicPublish(
                exchangeName, routingKey, body: body);
        });

    }


    public Dictionary<string, object> CreateDeadLetterQueue(IModel model)
    {
        model.ExchangeDeclare(
            ExchangeName,
            ExchangeType.Direct,
            true);

        model.QueueDeclare(
            QueueName,
            true,
            false,
            false);

        model.QueueBind(QueueName, ExchangeName, string.Empty);

        return _args;
    }

    public void Dispose()
    { 
        try
        {
            Channel?.Close();
            Channel?.Dispose();
            Channel = null;

            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Cannot dispose RabbitMQ channel or connection");
        }
    }
}