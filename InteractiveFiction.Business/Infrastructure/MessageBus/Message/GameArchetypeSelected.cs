using InteractiveFiction.Business.Infrastructure.Game;

namespace InteractiveFiction.Business.Infrastructure.MessageBus.Message
{
    public class GameArchetypeSelected : IMessage
    {
        public GameArchetype GameArchetype { get; }

        public GameArchetypeSelected(GameArchetype gameArchetype)
        {
            this.GameArchetype = gameArchetype;
        }
    }
}
