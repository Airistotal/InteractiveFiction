using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Procedure.Argument;

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
