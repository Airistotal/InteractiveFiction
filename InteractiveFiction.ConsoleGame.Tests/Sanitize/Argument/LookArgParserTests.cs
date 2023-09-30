using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure.Investigate;
using InteractiveFiction.ConsoleGame.Sanitize.Argument;

namespace InteractiveFiction.ConsoleGame.Tests.Sanitize.Argument
{
    public class LookArgParserTests
    {
        private static IEnumerable<object[]> Data()
        {
            yield return new object[] { new List<string> { "w", "west", "WeSt" }, new LookArg(Direction.West, "") };
            yield return new object[] { new List<string> { "e", "east", "EaSt" }, new LookArg(Direction.East, "") };
            yield return new object[] { new List<string> { "s", "south", "SoUtH" }, new LookArg(Direction.South, "") };
            yield return new object[] { new List<string> { "n", "north", "NoRtH" }, new LookArg(Direction.North, "") };
            yield return new object[] { new List<string> { "" }, new LookArg(Direction.NULL, "") };
            yield return new object[] { new List<string> { "target" }, new LookArg(Direction.NULL, "target") };
            yield return new object[] { new List<string> { "tfdal target" }, new LookArg(Direction.NULL, "target") };
            yield return new object[] { new List<string> { "n target" }, new LookArg(Direction.North, "target") };
            yield return new object[] { new List<string> { "w target" }, new LookArg(Direction.West, "target") };
            yield return new object[] { new List<string> { "e target" }, new LookArg(Direction.East, "target") };
            yield return new object[] { new List<string> { "s target" }, new LookArg(Direction.South, "target") };
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void When_ParseInput_ReturnsLookTarget(List<string> inputs, LookArg arg)
        {
            var sut = new LookArgParser();

            foreach (var input in inputs)
            {
                var parsed = sut.Parse(input.Split(" ").ToList());

                Assert.NotEmpty(parsed);
                var lkParsed = ArgCastUtils.AssertCastArg<LookArg>(parsed[0]);
                Assert.Equal(arg.Direction, lkParsed.Direction);
                Assert.Equal(arg.TargetName, lkParsed.TargetName);
            }
        }
    }
}
