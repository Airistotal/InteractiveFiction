using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Tests.Procedure
{
    public class NullProcedureTests
    {
        [Fact]
        public void NullProcedureDoesNothing()
        {
            var sut = new NullProcedure();

            sut.With(new List<IProcedureArg>()).Perform();
        }
    }
}
