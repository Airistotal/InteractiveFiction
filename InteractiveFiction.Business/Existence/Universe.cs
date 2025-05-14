using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Existence
{
    public class Universe : IUniverse
    {
        private readonly Location spawn;
        private readonly List<IProcedure> stack = new();
        private readonly List<ILaw> laws = new();

        public Universe(Location spawn)
        {
            this.spawn = spawn;
        }

        public void RegisterLaw(ILaw law)
        {
            this.laws.Add(law);
        }

        public void Put(IProcedure procedure)
        {
            this.stack.Add(procedure);
        }

        public void Tick()
        {
            foreach (ILaw law in laws)
            {
                law.apply();
            }

            foreach (IProcedure procedure in stack)
            {
                procedure.Perform();
            }

            this.stack.Clear();
        }

        public Instant GetInstant()
        {
            return new Instant(spawn);
        }

        public void Spawn(IEntity entity)
        {
            entity.SetLocation(spawn);
            entity.SetUniverse(this);
            entity.AddEvent($"{spawn.GetFullDescription()}");
            spawn.Children.Add(entity);
        }
    }
}
