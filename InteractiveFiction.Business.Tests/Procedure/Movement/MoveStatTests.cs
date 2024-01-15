using InteractiveFiction.Business.Goal;

namespace InteractiveFiction.Business.Tests.Procedure.Movement
{
    public class MoveStatTests
    {

        [Fact]
        public void When_MoveStat_InitializesAsOne()
        {
            MoveStatFixture.GetFixture()

                .Create("locationName")

                .AssertLocationVisits("locationName", 1);
        }

        [Fact]
        public void When_MoveStat_GetNotExists_ReturnsZero()
        {
            MoveStatFixture.GetFixture()

                .Create("locationName")

                .AssertLocationVisits("Not Visited", 0);
        }

        [Fact]
        public void When_MoveStat_AddTwoLocations_SameName_ReturnsTwo()
        {
            MoveStatFixture.GetFixture()

                .Add("location", "location")

                .AssertLocationVisits("location", 2);
        }

        [Fact]
        public void When_MoveStat_AddTwoLocations_DifferentName_ReturnsDifferentOnes()
        {
            MoveStatFixture.GetFixture()

                .Add("locationOne", "locationTwo")

                .AssertLocationVisits("locationOne", 1)
                .AssertLocationVisits("locationTwo", 1);
        }

        [Fact]
        public void When_MoveStat_AddOtherStat_ThrowsException()
        {
            MoveStatFixture.GetFixture()

                .AssertAddBadTypeThrowsException();
        }
    }
}
