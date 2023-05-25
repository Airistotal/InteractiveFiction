using InteractiveFiction.Business.Existence;

namespace InteractiveFiction.Business.Entity
{
    public class NullLocation : Location
    {
        private static NullLocation? _instance;

        public static NullLocation Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NullLocation();
                }

                return _instance;
            }
        }

        private NullLocation()
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            : base(null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            Type = LocationType.NULL;
            Title = "";
            Description = "This place doesn't exist.";
        }

        public override string GetFullDescription()
        {
            return Description ?? "";
        }
    }
}
