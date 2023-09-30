using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity.AnimateEntities
{
    public abstract class AnimateEntity: BaseAgent
    {
        protected AnimateEntity(IProcedureBuilder procedureBuilder) : base(procedureBuilder)
        {
        }

        // Identity Attributes
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public string Birthdate { get; set; } = "";

        public List<AnimateEntity> Parents { get; } = new List<AnimateEntity>();

        public List<AnimateEntity> Children { get; } = new List<AnimateEntity>();

        // Physical Attributes
        public int Health { get; set; }

        public int Strength { get; set; }

        public int Speed { get; set; }

        public int Dexterity { get; set; }

        public int Endurance { get; set; }

        // Mental Attributes
        public int Restraint { get; set; }

        public int Discretion { get; set; }

        public int Courage { get; set; }

        public int Fairness { get; set; }

        public int Compassion { get; set; }

        public int Hope { get; set; }

        public int Groundedness { get; set; }
    }
}
