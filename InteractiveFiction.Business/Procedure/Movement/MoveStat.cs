using InteractiveFiction.Business.Goal;
using InteractiveFiction.Business.Goal.Statistics;

namespace InteractiveFiction.Business.Procedure.Movement
{
    public class MoveStat : IStat
    {
        private readonly IDictionary<string, int> locationMoves = new Dictionary<string, int>();

        public MoveStat(string locationName) 
        {
            locationMoves.Add(locationName, 1);
        }

        public void Add(IStat stat)
        {
            if (stat is MoveStat moveStat) {
                foreach(var move in moveStat.locationMoves)
                {
                    if (locationMoves.ContainsKey(move.Key))
                    {
                        locationMoves[move.Key] += move.Value;
                    }
                    else
                    {
                        locationMoves.Add(move.Key, move.Value);
                    }
                }
            } 
            else
            {
                throw new StatAdditionException(typeof(MoveStat), stat.GetType());
            }
        }

        public int Get(string locationName)
        {
            if (locationMoves.TryGetValue(locationName, out int result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
    }
}
