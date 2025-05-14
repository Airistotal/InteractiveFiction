using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Existence;
using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Procedure;
using InteractiveFiction.Business.Tests.Utils;
using Moq;
using System;

namespace InteractiveFiction.Business.Tests.Entity
{
    internal class BaseAgentFixture
    {
        private BaseAgent sut;

        private Mock<IUniverse> universe;
        private Mock<IObserver<IStat>> observer;
        private Mock<IObservable<IStat>> observable;
        private Mock<IProcedure> procedure;

        private IList<ProcedureType> capabilities;

        private string evt = "text";

        private Location location;

        private bool isObservable;

        private BaseAgentFixture() {
            observer = new Mock<IObserver<IStat>>();
            universe = new Mock<IUniverse>();
            universe.Setup(_ => _.Put(It.IsAny<IProcedure>()));
            location = GetLocation("fdsa");
            capabilities = new List<ProcedureType>();
        }

        public static BaseAgentFixture GetFixture() { return new BaseAgentFixture(); }

        public BaseAgentFixture InstantiateEmpty()
        {
            sut = new Mock<BaseAgent>(null, null).Object;

            return this;
        }

        internal BaseAgentFixture WithObservableCapability(ProcedureType move)
        {
            isObservable = true;
            capabilities.Add(move);

            return this;
        }

        internal BaseAgentFixture WithCapability(ProcedureType move)
        {
            capabilities.Add(move);

            return this;
        }

        public BaseAgentFixture AddEvent()
        {
            Instantiate();

            sut.AddEvent(evt);

            return this;
        }

        public BaseAgentFixture AddAndArchiveEvents()
        {
            Instantiate();

            sut.AddEvent(evt);
            sut.ArchiveEvents();

            return this;

        }

        public BaseAgentFixture PerformMove()
        {
            Instantiate();

            sut.Perform(ProcedureType.Move, new List<IProcedureArg>());

            return this;
        }

        public BaseAgentFixture AddCapabilities()
        {
            Instantiate();

            return this; 
        }
        public BaseAgentFixture SetLocation()
        {
            Instantiate();

            sut.SetLocation(location);

            return this;
        }

        private Location GetLocation(string Title)
        {
            return new Location(DefaultMocks.GetTextDecorator().Object)
            {
                Title = Title
            };
        }

        private void Instantiate()
        {
            BuildMockProcedure();
            sut = new Mock<BaseAgent>(
                observer.Object,
                DefaultMocks.GetProcedureBuilderMock(procedure).Object).Object;
            sut.Universe = universe.Object;
            foreach (ProcedureType type in capabilities)
            {
                sut.AddCapability(type);
            }
        }

        private void BuildMockProcedure()
        {
            if (isObservable)
            {
                observable = new Mock<IObservable<IStat>>();
                procedure = observable.As<IProcedure>();
            }
            else
            {
                procedure = new Mock<IProcedure>();
            }

            procedure.Setup(_ => _.With(It.IsAny<IList<IProcedureArg>>())).Returns(procedure.Object);
        }

        public void AssertAddCapabilityThrowsException()
        {
            Assert.Throws<Exception>(() => sut.AddCapability(ProcedureType.Move));
        }

        public void AssertPerformThrowsException()
        {
            Assert.Throws<Exception>(() => sut.Perform(ProcedureType.Move, new List<IProcedureArg>()));
        }

        public void AssertUniverseNeverPutsProcedure()
        {
            universe.Verify(_ => _.Put(It.IsAny<IProcedure>()), Times.Never);
        }

        public void AssertUniversePutsProcedure()
        {
            universe.Verify(_ => _.Put(It.IsAny<IProcedure>()), Times.Once);
        }

        public void AssertEventAdded()
        {
            var events = sut.GetNewEvents();
            Assert.Contains(evt, events);
        }

        public void AssertEventsArchived()
        {
            Assert.Empty(sut.GetNewEvents());
        }

        public void AssertIncapableOfProcedureEvent()
        {
            Assert.Contains(sut.GetNewEvents(), _ => _.Contains("You can't"));
        }

        internal void AssertLocationSet()
        {
            Assert.Equal(location, sut.Location);
        }

        internal void AssertProcedureSubscribed()
        {
            observable.Verify(_ => _.Subscribe(observer.Object), Times.Once);
        }

        internal void AssertProcedureWasObserved()
        {
            if (observable != null)
            {
                observable.Verify(x => x.Subscribe(It.IsAny<IObserver<IStat>>()), Times.Once);
            } else
            {
                Assert.False(true);
            }
        }
    }
}
