using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Goal.Statistics;
using InteractiveFiction.Business.Procedure.Movement;
using Moq;

namespace InteractiveFiction.Business.Tests.Procedure.Movement
{
    internal class MoveStatFixture
    {
        private MoveStat sut;

        private MoveStatFixture() { }

        internal static MoveStatFixture GetFixture()
        {
            return new MoveStatFixture();
        }

        internal MoveStatFixture Create(string locationName)
        {
            sut = new MoveStat(locationName);

            return this;
        }

        internal MoveStatFixture Add(string locationName, string locationName2)
        {
            sut = new MoveStat(locationName);
            sut.Add(new MoveStat(locationName2));

            return this;
        }

        internal MoveStatFixture AssertLocationVisits(string locationName, int expectedVisits)
        {
            Assert.Equal(expectedVisits, sut.Get(locationName));

            return this;
        }

        internal void AssertAddBadTypeThrowsException()
        {
            sut = new MoveStat("");
            var other = new Mock<IStat>();
            Assert.Throws<StatAdditionException>(() => sut.Add(other.Object));
        }
    }
}