using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using EnumerableExtensions;

namespace EnumerablePositionExtensions
{
    public static class IEnumerablePositionExtensions
    {
        public static bool AllPositionsInSameRowOrColumn(this IEnumerable<Position> positions)
        {
            return positions.AllEqual(equalityComparison: Position.HaveSameRow) ||
                    positions.AllEqual(equalityComparison: Position.HaveSameColumn);
        }

        public static (Position, Position) FindMinAndMax(this IEnumerable<Position> positions)
        {
            var min = positions.First();
            var max = min;
            foreach (var position in positions)
            {
                if (position <= min) min = position;
                if (position >= max) max = position;
            }
            return (min, max);
        }
    }
}
