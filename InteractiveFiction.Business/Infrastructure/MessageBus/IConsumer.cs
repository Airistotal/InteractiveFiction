using InteractiveFiction.Business.Infrastructure.MessageBus.Message;

namespace InteractiveFiction.Business.Infrastructure.MessageBus
{
    public interface IConsumer
    {
        void Consume(IMessage message);
    }
}
