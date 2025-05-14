using InteractiveFiction.Business.Infrastructure.MessageBus.Message;

namespace InteractiveFiction.Business.Infrastructure.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> consumers = new();

        private MessageBus() { }

        private static IMessageBus? _messageBus;

        public static IMessageBus GetMessageBus()
        {
            if (_messageBus == null)
            {
                _messageBus = new MessageBus();
            }

            return _messageBus;
        }

        public void Publish(IMessage message)
        {
            if (!consumers.ContainsKey(message.GetType()))
            {
                return;
            }

            foreach (var consumer in consumers[message.GetType()])
            {
                consumer(message);
            }
        }

        public void Register<T>(Action<IMessage> consumer) where T : IMessage
        {
            if (!consumers.ContainsKey(typeof(T)))
            {
                consumers.Add(typeof(T), new List<Action<IMessage>>());
            }

            consumers[typeof(T)].Add(consumer);
        }
    }
}
