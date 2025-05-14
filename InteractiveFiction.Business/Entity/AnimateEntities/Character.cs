using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Entity.AnimateEntities
{
    public class Character : AnimateEntity
    {
        public Character(IProcedureBuilder procedureBuilder, ITextDecorator textDecorator)
            : base(procedureBuilder, textDecorator)
        {
        }

        public override string GetFullDescription()
        {
            return Name + Environment.NewLine + Description;
        }

        public override bool Is(string id)
        {
            return !string.IsNullOrWhiteSpace(Name) && Name.Equals(id);
        }
    }
}
