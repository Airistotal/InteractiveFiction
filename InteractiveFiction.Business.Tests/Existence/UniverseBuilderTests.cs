namespace InteractiveFiction.Business.Tests.Existence
{
    public class UniverseBuilderTests
    {
        [Fact]
        public void WhenCreateErogundPlacesLocation()
        {
            UniverseBuilderFixture.GetFixture()
                .ForRegion("Erogund")

                .CreateUniverse()

                .AssertErogundLocationsPlacedProperly();
        }

        [Fact]
        public void WhenCreateErogundPlacesAnimateEntities()
        {
            UniverseBuilderFixture.GetFixture()
                .ForRegion("Erogund")

                .CreateUniverse()

                .AssertCastleHasKing();
        }

        [Fact]
        public void WhenCreateArboraPlacesLocations()
        {
            UniverseBuilderFixture.GetFixture()
                .ForRegion("Arbora")

                .CreateUniverse()

                .AssertArboraLocationsPlacedProperly();
        }

        [Fact]
        public void WhenCreateArboraPlacesAnimateEntities()
        {
            UniverseBuilderFixture.GetFixture()
                .ForRegion("Arbora")

                .CreateUniverse()

                .AssertDenHasKobolds();
        }
    }
}
