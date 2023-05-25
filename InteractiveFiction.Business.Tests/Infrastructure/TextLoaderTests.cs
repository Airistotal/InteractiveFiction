using InteractiveFiction.Business.Infrastructure;

namespace InteractiveFiction.Business.Tests.Infrastructure
{
    public class TextLoaderTests
    {
        [Fact]
        public void When_TextLoader_GetTextNotFound_ReturnsKey()
        {
            var sut = new TextLoader();

            var result = sut.GetText("key");

            Assert.Equal("key", result);
        }

        [Fact]
        public void When_TextLoader_GetTextFound_ReturnsTranslation()
        {
            var sut = new TextLoader();

            var result = sut.GetText("found");
            var result2 = sut.GetText("found_obj.found");

            Assert.Equal("found text", result);
            Assert.Equal("found text", result2);
        }
    }
}
