using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity
{
    public abstract class AnimateEntity : BaseEntity
    {

        // Identity Attributes
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public string Birthdate { get; set; } = "";

        public List<AnimateEntity> Parents { get; } = new List<AnimateEntity>();

        public new List<AnimateEntity> Children { get; } = new List<AnimateEntity>();

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

        protected AnimateEntity(IProcedureBuilder? procedureBuilder) : base(procedureBuilder)
        {
        }

        public void SetLocation(Location location)
        {
            this.Location = location;

            if (Capabilities.ContainsKey(ProcedureType.Move))
            {
                Capabilities.Remove(ProcedureType.Move);
            }

            AddCapability(ProcedureType.Move, this.Location);
        }
    }
}
