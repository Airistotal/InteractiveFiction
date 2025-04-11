using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Infrastructure.MessageBus;

namespace InteractiveFiction.Business.Infrastructure.Game
{
    public class GameContainerFactory : IFactory<GameType, IGameContainer>
    {
        private readonly IMessageBus messageBus;
        private readonly IUniverseBuilder universeBuilder;
        private readonly IEntityBuilder entityBuilder;

        public GameContainerFactory(
            IMessageBus messageBus,
            IUniverseBuilder universeBuilder,
            IEntityBuilder entityBuilder)
        {
            this.messageBus = messageBus;
            this.universeBuilder = universeBuilder;
            this.entityBuilder = entityBuilder;
        }

        public IGameContainer Create(GameType type)
        {
            if (type == GameType.Generic)
            {
                return new GameContainer(messageBus, universeBuilder, entityBuilder);
            } 
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
