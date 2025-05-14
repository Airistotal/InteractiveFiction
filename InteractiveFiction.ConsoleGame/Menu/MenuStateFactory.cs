using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Infrastructure.MessageBus;
using InteractiveFiction.ConsoleGame.Menu.State;

namespace InteractiveFiction.ConsoleGame.Menu
{
    public class MenuStateFactory : IMenuStateFactory
    {
        private readonly IMessageBus messageBus;
        private readonly ITextLoader textLoader;
        private readonly IGameArchetypeLoader gameArchetypeLoader;

        public MenuStateFactory( ITextLoader textLoader, IGameArchetypeLoader gameArchetypeLoader, IMessageBus messageBus)
        {
            this.textLoader = textLoader;
            this.gameArchetypeLoader = gameArchetypeLoader;
            this.messageBus = messageBus;
        }

        public IMenuState GetInstance(MenuStateType type)
        {
            return type switch
            {
                MenuStateType.MainMenu => new MainMenuState(this, textLoader),
                MenuStateType.NewGame => new NewGameMenuState(this, messageBus, textLoader, gameArchetypeLoader),
                MenuStateType.NewGameName => new NewGameNameMenuState(this, messageBus, textLoader),
                MenuStateType.LoadGame => new LoadGameMenuState(),
                MenuStateType.CreateCharacter => new CreateCharacterMenuState(this, messageBus, textLoader),
                MenuStateType.CharacterSelect => new CharacterSelectMenuState(),
                _ => throw new Exception("Unable to create MenuState " + type),
            };
        }
    }
}
 