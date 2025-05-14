using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Procedure;
using Moq;

namespace InteractiveFiction.Business.Tests.Utils
{
    public class TestAnimateEntity : AnimateEntity
    {
        public Mock<IProcedureBuilder> builderMock { get; }

        public TestAnimateEntity() : this(DefaultMocks.GetProcedureBuilderMock()) { }

        public TestAnimateEntity(Mock<IProcedureBuilder> builderMock) : base(builderMock.Object)
        {
            this.builderMock = builderMock;
        }
    }
}
