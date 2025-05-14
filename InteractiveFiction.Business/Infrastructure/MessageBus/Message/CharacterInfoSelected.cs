namespace InteractiveFiction.Business.Infrastructure.MessageBus.Message
{
    public class CharacterInfoSelected : IMessage
    {
        public string Name { get; set; }
    }
}
