using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.Business.Infrastructure.MessageBus.Message;
using InteractiveFiction.Business.Procedure;
using System.Diagnostics.CodeAnalysis;

namespace InteractiveFiction.Business.Infrastructure.Game
{
    public class GameContainer : IGameContainer
    {
        private readonly IMessageBus messageBus;
        private readonly IUniverseBuilder universeBuilder;
        private readonly IEntityBuilder entityBuilder;

        private IUniverse? universe;
        private IEntity? character;
        private IAgent? characterAgent;

        public GameContainer(
            IMessageBus messageBus,
            IUniverseBuilder universeBuilder,
            IEntityBuilder entityBuilder)
        {
            this.messageBus = messageBus;
            this.universeBuilder = universeBuilder;
            this.entityBuilder = entityBuilder;

            this.messageBus.Register<GameArchetypeSelected>(HandleGameArchetypeSelected);
            this.messageBus.Register<CharacterInfoSelected>(HandleCharacterInfoSelected);
        }

        public void HandleGameArchetypeSelected(IMessage message)
        {
            if (message is GameArchetypeSelected archetypeSelected)
            {
                if (!string.IsNullOrEmpty(archetypeSelected.GameArchetype.Name))
                {
                    universe = universeBuilder.Create(archetypeSelected.GameArchetype.Name);
                }
            }

            MoveToGameIfReady();
        }

        public void HandleCharacterInfoSelected(IMessage message)
        {
            if (message is CharacterInfoSelected characterInfo)
            {
                character = entityBuilder.Character(characterInfo.Name).Build();
                
                if (character is IAgent characterAgent)
                {
                    this.characterAgent = characterAgent;
                } else
                {
                    throw new Exception("The created entity must also be an agent.");
                }
            }

            MoveToGameIfReady();
        }

        private void MoveToGameIfReady()
        {
            if (universe != null && characterAgent != null)
            {
                universe.Spawn(characterAgent);
                messageBus.Publish(new MoveToGameMessage());
            }
        }

        public string GetScreen()
        {
            CheckIsReady();

            var events = characterAgent.GetNewEvents();
            characterAgent.ArchiveEvents();

            return string.Join(Environment.NewLine, events);
        }

        public void Perform(ProcedureCommand command)
        {
            CheckIsReady();

            characterAgent.Perform(command.ProcedureType, command.Args);
        }

        [MemberNotNull(nameof(character))]
        [MemberNotNull(nameof(characterAgent))]
        [MemberNotNull(nameof(universe))]
        private void CheckIsReady()
        {
            if (universe == null || character == null || characterAgent == null)
            {
                throw new Exception("No character or universe!");
            }
        }

        public bool IsReady()
        {
            return universe != null && character != null;
        }

        public void Tick()
        {
            CheckIsReady();

            universe.Tick();
        }

        public SaveData GetSaveData()
        {
            return new SaveData() {
                Universe = universe,
                Character = character,
                CharacterAgent = characterAgent,
            };
        }

        public void Load(SaveData data)
        {
            universe = data.Universe;
            character = data.Character;
            characterAgent = data.CharacterAgent;
        }
    }
}
