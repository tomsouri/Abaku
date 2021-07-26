using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace BoardController
{
    namespace Validator
    {
        namespace IEnumerablePositionExtensions
        {
            internal static class IEnumerablePositionExtensions
            {
                public static bool ArePositionsPairwiseDistinct(this IEnumerable<Position> positions)
                {
                    int positionsCount = 0;
                    int equalitiesCount = 0;
                    foreach (var pos1 in positions)
                    {
                        positionsCount++;
                        foreach (var pos2 in positions)
                        {
                            if (pos1 == pos2) equalitiesCount++;
                        }
                    }
                    // Every position is equal to itself.
                    // If the positions are distinct, the number of equalities
                    // is the same as the number of positions.
                    return positionsCount == equalitiesCount;
                }
                public static bool AllPositionsInSameRowOrColumn(this IEnumerable<Position> positions)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
