using Fitness.Common.EventStore.Events;

namespace Fitness.Common.RabbitMQ
{
    public class RabbitEventListener : IRabbitEventListener
    {
        public RabbitEventListener()
        {
            
        }
        public void Subscribe(Type type)
        {
        }

        public void Subscribe<TEvent>() where TEvent : IEvent
        {
            Subscribe(typeof(TEvent));
        }

        public Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            return Task.CompletedTask;
        }

        public Task Publish(string message, string type)
        {
            return Task.CompletedTask;
        }
    }
}
