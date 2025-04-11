using InteractiveFiction.Business.Procedure.Combat;
using InteractiveFiction.ConsoleGame.Sanitize.Argument;

namespace InteractiveFiction.ConsoleGame.Tests.Sanitize.Argument
{
    public class AttackArgParserTests
    {
        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { "", new AttackArg("") };
            yield return new object[] { "2", new AttackArg("2") };
            yield return new object[] { "2afds", new AttackArg("2afds") };
            yield return new object[] { "$&HNsbl", new AttackArg("$&HNsbl") };
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void When_ParseAttackArg_SetsTarget(string input, AttackArg arg)
        {
            var sut = new AttackArgParser();

            var parsed = sut.Parse(new List<string> { input });

            Assert.NotEmpty(parsed);
            var atkParsed = ArgCastUtils.AssertCastArg<AttackArg>(parsed[0]);
            Assert.Equal(arg.TargetName, atkParsed.TargetName);
        }
    }
}
