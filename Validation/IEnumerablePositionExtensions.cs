﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace Validation
{
    internal static class IEnumerablePositionExtensions
    {
        internal static bool ArePositionsPairwiseDistinct(this IEnumerable<Position> positions)
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
        internal static bool AllPositionsInSameRowOrColumn(this IEnumerable<Position> positions)
        {
            return positions.AllEqual(equalityComparison: Position.HaveSameRow) ||
                    positions.AllEqual(equalityComparison: Position.HaveSameColumn);
        }

        /// <summary>
        /// Are all Positions in the collection equal, compared using delegate equalityComparison?
        /// </summary>
        /// <typeparam name="Position"></typeparam>
        /// <param name="positions"></param>
        /// <param name="equalityComparison"></param>
        /// <returns>True if all Positions are equal.</returns>
        internal static bool AllEqual<Position>(this IEnumerable<Position> positions, Func<Position, Position, bool> equalityComparison)
        {
            if (!positions.Any()) return true;
            Position first = positions.First();
            return positions.Skip(1).All(p => equalityComparison(first, p));
        }

        internal static (Position, Position) FindMinAndMax(this IEnumerable<Position> positions)
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

        internal static bool AtLeastOneSatisfies(this IEnumerable<Position> positions, Predicate<Position> predicate)
        {
            bool isSatisfied = false;
            foreach (var position in positions)
            {
                if (predicate(position)) isSatisfied = true;
            }
            return isSatisfied;
        }
    }
}