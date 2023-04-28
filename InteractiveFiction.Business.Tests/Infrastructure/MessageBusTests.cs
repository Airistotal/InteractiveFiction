using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;
using Moq;

namespace InteractiveFiction.Business.Tests.Infrastructure
{
    public class MessageBusTests
    {
        [Fact]
        public void When_MessageBusPublishesEvent_CallsAppropriateConsumer()
        {
            var consumer = new TestConsumer();
            var consumer2 = new TestConsumer();
            var sut = MessageBus.GetMessageBus();

            sut.Register<TestMessage>(consumer.Consume);
            sut.Register<TestMessage2>(consumer2.Consume);
            sut.Publish(new TestMessage());

            Assert.True(consumer.called);
            Assert.False(consumer2.called);
        }

        public class TestMessage : IMessage { }
        public class TestMessage2 : IMessage { }

        public class TestConsumer : IConsumer
        {
            public bool called = false;

            public void Consume(IMessage message)
            {
                called = true;
            }
        }
    }
}
