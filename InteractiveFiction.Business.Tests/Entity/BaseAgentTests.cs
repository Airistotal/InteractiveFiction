using InteractiveFiction.Business.Procedure;

namespace InteractiveFiction.Business.Tests.Entity
{
    public class BaseAgentTests
    {
        [Fact]
        public void WhenUniverseEntityDoesProcedureWithoutUniverse_ThrowsException()
        {
            BaseAgentFixture.GetFixture()

                .InstantiateEmpty()

                .AssertPerformThrowsException();
        }

        [Fact]
        public void WhenAddCapabilityWithoutProcedureBuilder_ThrowsException()
        {
            BaseAgentFixture.GetFixture()

                .InstantiateEmpty()

                .AssertAddCapabilityThrowsException();
        }

        [Fact]
        public void WhenEntityDoesProcedureNotInCapabilities_Ignores()
        {
            BaseAgentFixture.GetFixture()

                .PerformMove()

                .AssertUniverseNeverPutsProcedure();
        }

        [Fact]
        public void WhenEntityDoesProcedureInCapabilities_SendsToUniverse()
        {
            BaseAgentFixture.GetFixture()
                .WithCapability(ProcedureType.Move)

                .PerformMove()

                .AssertUniversePutsProcedure();
        }

        [Fact]
        public void WhenEntityAddsProcedure_ObservesProcedure()
        {
            BaseAgentFixture.GetFixture()
                .WithObservableCapability(ProcedureType.Move)

                .AddCapabilities()

                .AssertProcedureSubscribed();
        }

        [Fact]
        public void WhenPerformProcedure_NotFound_AddsEvent()
        {
            BaseAgentFixture.GetFixture()

                .PerformMove()

                .AssertIncapableOfProcedureEvent();
        }

        [Fact]
        public void WhenAddEvent_AddsToNewEvents()
        {
            BaseAgentFixture.GetFixture()

                .AddEvent()

                .AssertEventAdded();
        }

        [Fact]
        public void WhenArchiveEvents_RemovesEvents()
        {
            BaseAgentFixture.GetFixture()

                .AddAndArchiveEvents()

                .AssertEventsArchived();
        }

        [Fact]
        public void WhenAgentChangesLocation_UpdatesLocation()
        {
            BaseAgentFixture.GetFixture()

                .SetLocation()

                .AssertLocationSet();
        }

        [Fact]
        public void WhenPerformCapability_IsObservable_ObservesProcedure()
        {
            BaseAgentFixture.GetFixture()
                .WithObservableCapability(ProcedureType.Move)

                .PerformMove()

                .AssertProcedureWasObserved();
        }
    }
}
