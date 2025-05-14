using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;

namespace InteractiveFiction.Business.Infrastructure.Game
{
    public class SaveData
    {
        public IUniverse? Universe { get; set; }
        public IEntity? Character { get; set; }
        public IAgent? CharacterAgent { get; set; }
    }
}