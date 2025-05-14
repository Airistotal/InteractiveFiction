namespace InteractiveFiction.Business.Infrastructure.MessageBus.Message
{
    public class CharacterNameSelected : IMessage
    {
        public string Name { get; set; }
    }
}
