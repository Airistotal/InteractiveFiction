using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.AnimateEntities;
using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Goal.Questing;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Entity.AnimateEntities
{
    public class CharacterTests
    {
        [Fact]
        public void When_GetDescription_ReturnsNameAndDesc()
        {
            var sut = new Character(
                new Mock<IQuestManager>().Object,
                new Mock<IObserver<IStat>>().Object,
                DefaultMocks.GetProcedureBuilderMock().Object)
            {
                Name = "fdsa",
                Description = "zxcv"
            };

            var desc = sut.GetFullDescription();

            Assert.Contains("fdsa", desc);
            Assert.Contains("zxcv", desc);
        }

        [Fact]
        public void When_ReceiveDamage_SubtractsAmount()
        {
            var sut = new Character(
                new Mock<IQuestManager>().Object,
                new Mock<IObserver<IStat>>().Object, 
                DefaultMocks.GetProcedureBuilderMock().Object);
            var health = sut.Health;

            sut.ReceiveDamage(new Mock<IDamager>().Object, 1);

            Assert.Equal(health - 1, sut.Health);
        }
    }
}
