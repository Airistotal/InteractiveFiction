﻿using InteractiveFiction.Business.Entity.AnimateEntities;
using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Tests.Utils;
using Moq;

namespace InteractiveFiction.Business.Tests.Entity.AnimateEntities
{
    public class AnimateEntityTests
    {
        [Fact]
        public void WhenAnimateEntityCreatedHasPhysicalAttributes()
        {
            var sut = GetAnimateEntity();

            Assert.Equal(0, sut.Health);
            Assert.Equal(0, sut.Strength);
            Assert.Equal(0, sut.Dexterity);
            Assert.Equal(0, sut.Endurance);
            Assert.Equal(0, sut.Speed);
        }

        [Fact]
        public void WhenAnimateEntityCreatedHasMentalAttributes()
        {
            var sut = GetAnimateEntity();

            Assert.Equal(0, sut.Restraint);
            Assert.Equal(0, sut.Discretion);
            Assert.Equal(0, sut.Courage);
            Assert.Equal(0, sut.Fairness);
            Assert.Equal(0, sut.Compassion);
            Assert.Equal(0, sut.Hope);
            Assert.Equal(0, sut.Groundedness);
        }

        [Fact]
        public void WhenAnimateEntityCreatedHasIdentityAttributes()
        {
            var sut = GetAnimateEntity();

            Assert.Equal("", sut.Name);
            Assert.Equal("", sut.Description);
            Assert.Equal("", sut.Birthdate);
            Assert.Empty(sut.Parents);
            Assert.Empty(sut.Children);
            Assert.Equal(NullLocation.Instance, sut.Location);
        }

        private AnimateEntity GetAnimateEntity()
        {
            return new Mock<AnimateEntity>(
                new Mock<IObserver<IStat>>().Object,
                DefaultMocks.GetProcedureBuilderMock().Object).Object;
        }
    }
}
