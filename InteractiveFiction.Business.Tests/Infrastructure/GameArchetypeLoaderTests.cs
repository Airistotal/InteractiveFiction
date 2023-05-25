using InteractiveFiction.Business.Infrastructure;
using Moq;

namespace InteractiveFiction.Business.Tests.Infrastructure
{
    public class GameArchetypeLoaderTests
    {
        [Fact]
        public void When_LoadGameArchetypes_LooksAtDirectory()
        {
            var directoryFacade = new Mock<IDirectoryFacade>();
            var sut = new GameArchetypeLoader(directoryFacade.Object);
            directoryFacade.Setup(_ => _.GetDirectories("games")).Returns(new string[] { "GameName" });

            var archetypes = sut.LoadGameArchetypes();

            Assert.NotNull(archetypes);
            Assert.Single(archetypes);
            Assert.Equal("GameName", archetypes[0].Name);

            directoryFacade.Verify(_ => _.GetDirectories("games"), Times.Once);
        }
    }
}
