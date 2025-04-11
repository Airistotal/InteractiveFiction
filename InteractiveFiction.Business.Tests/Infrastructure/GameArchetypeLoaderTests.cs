using System.IO.Abstractions;
using InteractiveFiction.Business.Infrastructure.Game;
using Moq;

namespace InteractiveFiction.Business.Tests.Infrastructure
{
    public class GameArchetypeLoaderTests
    {
        [Fact]
        public void When_LoadGameArchetypes_LooksAtDirectory()
        {
            var fs = new Mock<IFileSystem>();
            var directory = new Mock<IDirectory>();
            fs.Setup(x => x.Directory).Returns(directory.Object);
            directory.Setup(x => x.GetDirectories("games")).Returns(new string[] { "GameName" });

            var sut = new GameArchetypeLoader(fs.Object);

            var archetypes = sut.LoadGameArchetypes();

            Assert.NotNull(archetypes);
            Assert.Single(archetypes);
            Assert.Equal("GameName", archetypes[0].Name);

            directory.Verify(_ => _.GetDirectories("games"), Times.Once);
        }
    }
}
