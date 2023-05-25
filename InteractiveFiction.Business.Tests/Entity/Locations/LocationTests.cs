using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Entity.Locations
{
    public class LocationTests
    {
        [Fact]
        public void WhenCreateWithPropertiesGetsProperties()
        {
            var sut = GetLocation();
            sut.Title = "Italy";
            sut.Description = "Mama mia!";

            Assert.Equal("Italy", sut.Title);
            Assert.Equal("Mama mia!", sut.Description);
        }

        [Fact]
        public void WhenAddChildToLocationCanHaveChildren()
        {
            var sut = GetLocation();
            sut.Children.Add(new Mock<IEntity>().Object);

            Assert.NotNull(sut.Children);
            Assert.NotEmpty(sut.Children);
        }

        [Fact]
        public void WhenAddPathGoHasLocationInDirection()
        {
            var location = GetLocation();
            var locationInDirection = GetLocation();

            location.AddPath(Direction.North, locationInDirection);
            var locInDir = location.Go(Direction.North);

            Assert.Equal(locationInDirection, locInDir);
        }

        [Fact]
        public void WhenAddNullPathIgnores()
        {
            var sut = GetLocation();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            sut.AddPath(Direction.North, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            var locInDir = sut.Go(Direction.North);

            Assert.Equal(NullLocation.Instance.Description, locInDir.Description);
        }

        [Fact]
        public void WhenDestroyPathGoReturnsNullLocationInDirection()
        {
            var location = GetLocation();
            var locationInDirection = GetLocation();

            location.AddPath(Direction.North, locationInDirection);
            location.DestroyPath(Direction.North);
            var locInDir = location.Go(Direction.North);

            Assert.Equal(NullLocation.Instance.Description, locInDir.Description);
        }

        [Fact]
        public void When_SameProperties_AreEqual()
        {
            Assert.Equal(NullLocation.Instance, NullLocation.Instance);
        }

        [Fact]
        public void When_GetDirections_ReturnsStringOfDirections()
        {
            var sut = GetLocation();
            sut.AddPath(Direction.North, NullLocation.Instance);
            sut.AddPath(Direction.West, NullLocation.Instance);

            var result = sut.GetDirections();

            Assert.Contains("North", result);
            Assert.Contains("West", result);
        }

        [Fact]
        public void When_GetDescription_GetsNameDescDirectionsAndChildren()
        {
            var child = new Mock<IEntity>();
            child.Setup(_ => _.GetName()).Returns("name");
            var sut = GetLocation();
            sut.Title = "fdsa";
            sut.Description = "zxcv";
            sut.Children.Add(child.Object);
            sut.AddPath(Direction.North, GetLocation());

            var result = sut.GetFullDescription();

            Assert.Contains("North", result);
            Assert.Contains(sut.Title, result);
            Assert.Contains(sut.Description, result);
            Assert.Contains("name", result);
        }

        [Fact]
        public void When_GetTargetFound_ReturnsTarget()
        {
            var target = new Mock<IEntity>();
            target.Setup(_ => _.Is(It.IsAny<string>())).Returns(true);
            var sut = GetLocation();
            sut.Children.Add(target.Object);

            var found = sut.GetTarget("target");

            Assert.Equal(target.Object, found);
        }

        [Fact]
        public void When_GetTargetNotFound_ReturnsNullEntity()
        {
            var target = new Mock<IEntity>();
            target.Setup(_ => _.Is(It.IsAny<string>())).Returns(false);
            var sut = GetLocation();
            sut.Children.Add(target.Object);

            var found = sut.GetTarget("target");

            Assert.Equal(NullEntity.Instance, found);
        }

        private Location GetLocation()
        {
            return new Location(DefaultMocks.GetTextDecorator().Object);
        }
    }
}
