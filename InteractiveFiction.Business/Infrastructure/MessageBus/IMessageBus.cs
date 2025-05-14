using InteractiveFiction.Business.Infrastructure.MessageBus.Message;

namespace InteractiveFiction.Business.Infrastructure.MessageBus
{
    public interface IMessageBus
    {
        void Publish(IMessage message);
        void Register<T>(Action<IMessage> consumer) where T : IMessage;
    }
}
