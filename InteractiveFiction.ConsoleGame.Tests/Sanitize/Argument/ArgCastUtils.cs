using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.ConsoleGame.Tests.Sanitize.Argument
{
    internal class ArgCastUtils
    {
        public static T AssertCastArg<T>(IProcedureArg arg) where T : IProcedureArg
        {
            Assert.IsType<T>(arg);

            if (arg is T lkArg)
            {
                return lkArg;
            }
            else
            {
                throw new Exception("FATAL");
            }
        }
    }
}
