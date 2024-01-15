using InteractiveFiction.Business.Entity.Locations;
using InteractiveFiction.Business.Infrastructure;
using InteractiveFiction.WorldBuilder.Business;
using InteractiveFiction.WorldBuilder.Business.World;
using Microsoft.AspNetCore.Components;

namespace InteractiveFiction.WorldBuilder.Pages.Edit
{
    public partial class EditWorld : ComponentBase
    {
        private World world = new("");
        private Location currentLocation = NullLocation.Instance;
        private ITextDecorator TextDecorator { get; }

        public EditWorld(ITextDecorator textDecorator)
        {
            this.TextDecorator = textDecorator;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var result = await ProtectedSessionStore.GetAsync<World>(SessionKey.WORKING_WORLD.ToString());
            if (!result.Success)
            {
                throw new WorldNotOpenedException();
            }

            world = result.Value;
        }

        public void AddLocation()
        {
            world.Locations.Add(new Location(TextDecorator));
        }
    }
}
