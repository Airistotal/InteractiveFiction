using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Combat;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Procedure.Combat
{
    public class AttackProcedureTests
    {
        [Fact]
        public void WhenAttackTarget_ReducesHealth()
        {
            var target = GetTarget();
            var agent = GetAgent(GetLocationWithTarget(target.Object));
            var sut = new AttackProcedure(agent.Object).With(new List<IProcedureArg>() { new AttackArg("target") });

            sut.Perform();

            agent.Verify(x => x.AddEvent(It.Is<string>(_ => _.Contains("You hit target for 1 damage"))));
            target.As<IDamageable>().Verify(x => x.ReceiveDamage(It.IsAny<IDamager>(), 1));
            target.As<IAgent>().Verify(x => x.AddEvent(It.Is<string>(_ => _.Contains("You were hit"))));
        }

        private Location GetLocationWithTarget(IEntity target)
        {
            var loc = new Location(DefaultMocks.GetTextDecorator().Object);
            loc.Children.Add(target);

            return loc;
        }

        private static Mock<IEntity> GetTarget()
        {
            var damageable = new Mock<IDamageable>();
            var agent = damageable.As<IAgent>();
            var target = agent.As<IEntity>();
            target.Setup(_ => _.GetName()).Returns("target");
            target.Setup(_ => _.Is(It.IsAny<string>())).Returns(true);

            return target;
        }

        private static Mock<IAgent> GetAgent(Location location)
        {
            var damager = new Mock<IDamager>();
            damager.Setup(_ => _.CalculateDamage()).Returns(1);
            var entity = damager.As<IEntity>();
            entity.Setup(_ => _.GetLocation()).Returns(location);
            var agent = entity.As<IAgent>();

            return agent;
        }
    }
}
