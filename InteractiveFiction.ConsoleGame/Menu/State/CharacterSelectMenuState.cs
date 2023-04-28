using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveFiction.ConsoleGame.Menu.State
{
    public class CharacterSelectMenuState : IMenuState 
    { 
        public CharacterSelectMenuState()
        {

        }

        public string GetScreen()
        {
            throw new NotImplementedException();
        }

        public IMenuState Transition(Command command, params string[] values)
        {
            throw new NotImplementedException();
        }
    }
}
