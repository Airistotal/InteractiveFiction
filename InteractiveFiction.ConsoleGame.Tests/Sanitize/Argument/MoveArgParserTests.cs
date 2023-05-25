using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Procedure.Argument;
using InteractiveFiction.ConsoleGame.Sanitize.Argument;

namespace InteractiveFiction.ConsoleGame.Tests.Sanitize.Argument
{
    public class MoveArgParserTests
    {
        private static IEnumerable<object[]> Data()
        {
            yield return new object[] { new List<string> { "w", "west", "WeSt" }, new MoveArg(Direction.West) };
            yield return new object[] { new List<string> { "e", "east", "EaSt" }, new MoveArg(Direction.East) };
            yield return new object[] { new List<string> { "s", "south", "SoUtH" }, new MoveArg(Direction.South) };
            yield return new object[] { new List<string> { "n", "north", "NoRtH" }, new MoveArg(Direction.North) };
            yield return new object[] { new List<string> { "i", "in", "In" }, new MoveArg(Direction.In) };
            yield return new object[] { new List<string> { "o", "out", "oUt" }, new MoveArg(Direction.Out) };
            yield return new object[] { new List<string> { "af", "" }, new MoveArg(Direction.NULL) };
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void When_ParseInput_ReturnsDirectionList(List<string> inputs, MoveArg arg)
        {
            var sut = new MoveArgParser();

            foreach (var input in inputs)
            {
                var parsed = sut.Parse(new List<string>() { input });

                Assert.NotEmpty(parsed);
                var mvParsed = AssertIsMoveArg(parsed[0]);
                Assert.Equal(arg.Direction, mvParsed.Direction);
            }
        }

        private static MoveArg AssertIsMoveArg(IProcedureArg arg)
        {
            Assert.IsType<MoveArg>(arg);

            if (arg is MoveArg mvArg)
            {
                return mvArg;
            } else
            {
                throw new Exception("FATAL");
            }
        }
    }
}
