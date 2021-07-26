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
                private static bool AllPositionsInSameRow(IEnumerable<Position> positions)
                {
                    bool sameRow = true;

                    // Assignment only because of CSC to allow using firstPosition in the else-branch.
                    Position firstPosition = new Position();

                    bool isFirst = true;

                    foreach (var position in positions)
                    {
                        if (isFirst)
                        {
                            firstPosition = position;
                            isFirst = false;
                        }
                        else
                        {
                            if (!position.IsSameRow(firstPosition)) sameRow = false;
                        }
                    }
                    return sameRow;
                }
                private static bool AllPositionsInSameColumn(IEnumerable<Position> positions)
                {
                    bool sameColumn = true;

                    // Assignment only because of CSC to allow using firstPosition in the else-branch.
                    Position firstPosition = new Position();

                    bool isFirst = true;

                    foreach (var position in positions)
                    {
                        if (isFirst)
                        {
                            firstPosition = position;
                            isFirst = false;
                        }
                        else
                        {
                            if (!position.IsSameColumn(firstPosition)) sameColumn = false;
                        }
                    }
                    return sameColumn;
                }
                public static bool AllPositionsInSameRowOrColumn(this IEnumerable<Position> positions)
                {
                    return AllPositionsInSameColumn(positions) | AllPositionsInSameRow(positions);

                }
            }
        }
    }
}
