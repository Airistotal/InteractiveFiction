using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Entity
{
    public class LocationTests
    {
        [Fact]
        public void WhenCreateWithPropertiesGetsProperties()
        {
            var sut = new Location(DefaultMocks.GetProcedureBuilderMock().Object) {
                Title = "Italy",
                Description = "Mama mia!"
            };

            Assert.Equal("Italy", sut.Title);
            Assert.Equal("Mama mia!", sut.Description);
        }

        [Fact]
        public void WhenAddChildToLocationCanHaveChildren()
        {
            Location location = new(DefaultMocks.GetProcedureBuilderMock().Object);
            location.Children.Add(new Mock<IEntity>().Object);

            Assert.NotNull(location.Children);
            Assert.NotEmpty(location.Children);
        }

        [Fact]
        public void WhenAddPathGoHasLocationInDirection()
        {
            Location location = new(DefaultMocks.GetProcedureBuilderMock().Object);
            Location locationInDirection = new(DefaultMocks.GetProcedureBuilderMock().Object);

            location.AddPath(Direction.North, locationInDirection);
            var locInDir = location.Go(Direction.North);

            Assert.Equal(locationInDirection, locInDir);
        }

        [Fact]
        public void WhenAddNullPathIgnores()
        {
            Location location = new(DefaultMocks.GetProcedureBuilderMock().Object);

            location.AddPath(Direction.North, null);
            var locInDir = location.Go(Direction.North);

            Assert.Equal(Location.Empty().Description, locInDir.Description);
        }

        [Fact]
        public void WhenDestroyPathGoReturnsNullLocationInDirection()
        {
            Location location = new(DefaultMocks.GetProcedureBuilderMock().Object);
            Location locationInDirection = new(DefaultMocks.GetProcedureBuilderMock().Object);

            location.AddPath(Direction.North, locationInDirection);
            location.DestroyPath(Direction.North);
            var locInDir = location.Go(Direction.North);

            Assert.Equal(Location.Empty().Description, locInDir.Description);
        }
    }
}
