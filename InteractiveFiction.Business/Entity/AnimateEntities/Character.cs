using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Goal.Questing;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity.AnimateEntities
{
    public class Character : AnimateEntity, IEntity, IAgent, IDamager, IDamageable
    {
        private IQuestManager questManager;

        public Character(IQuestManager questManager, IObserver<IStat> observer, IProcedureBuilder procedureBuilder)
            : base(observer, procedureBuilder)
        {
            this.questManager = questManager;
        }

        public string GetFullDescription()
        {
            return Name + Environment.NewLine + Description;
        }

        public bool Is(string id)
        {
            return !string.IsNullOrWhiteSpace(Name) && Name.Equals(id);
        }

        public string GetName()
        {
            return Name ?? "";
        }

        public void ReceiveDamage(IDamager perpetrator, int amount)
        {
            this.Health -= amount;
        }

        public int CalculateDamage()
        {
            return this.Strength;
        }

        public Guid GetId()
        {
            return Id;
        }
    }
}
