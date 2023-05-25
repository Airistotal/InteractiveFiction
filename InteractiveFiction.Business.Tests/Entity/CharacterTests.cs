﻿using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Tests.Utils;

namespace InteractiveFiction.Business.Tests.Entity
{
    public class CharacterTests
    {
        [Fact]
        public void When_GetDescription_ReturnsNameAndDesc()
        {
            var sut = new Character(
                DefaultMocks.GetProcedureBuilderMock().Object,
                DefaultMocks.GetTextDecorator().Object)
            {
                Name = "fdsa",
                Description = "zxcv"
            };

            var desc = sut.GetFullDescription();

            Assert.Contains("fdsa", desc);
            Assert.Contains("zxcv", desc);
        }
    }
}
