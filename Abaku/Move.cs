using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    /// <summary>
    /// Represents a move including the score you get for it.
    /// </summary>
    public struct EvaluatedMove
    {
        public EvaluatedMove(Move move, int score)
        {
            Move = move;
            Score = score;
        }
        public Move Move { get; }
        public int Score { get; }
    }

    /// <summary>
    /// Represents one move in a game of Abaku, that is the array of placed stones (digits) with their positions.
    /// Supports also the move evaluation, that is adding the point score of the move.
    /// </summary>
    public struct Move: IEnumerable<(Digit, Position)>
    {
        private readonly IReadOnlyList<Digit> PlacedDigits;
        private readonly IReadOnlyList<Position> UsedPositions;
        public Move(IReadOnlyList<Digit> placedDigits, IReadOnlyList<Position> usedPositions)
        {
            PlacedDigits = placedDigits;
            UsedPositions = usedPositions;
        }
        public Digit? this[Position position]
        {
            get
            {
                foreach (var (digit, pos) in this)
                {
                    if (position == pos) return digit;
                }
                return null;
            }
        }

        public bool ContainsPosition(Position position)
        {
            return UsedPositions.Contains(position);
        }
        public IReadOnlyList<Position> PositionsSorted
        {
            get
            {
                var sorted = UsedPositions.ToArray();
                Array.Sort(sorted, (a,b) => a.CompareTo(b));
                return sorted;
            }
        }

        public IEnumerable<Position> GetPositions()
        {
            return UsedPositions;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<(Digit, Position)> GetEnumerator()
        {
            for (int i = 0; i < PlacedDigits.Count; i++)
            {
                yield return (PlacedDigits[i], UsedPositions[i]);
            }
        }

        /// <summary>
        /// Determines whether the move places zero to the specified position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>True if the move contains the position and places zero there.</returns>
        public bool ContainsZero(Position position)
        {
            foreach (var (digit, pos) in this)
            {
                if (pos == position) return digit == Digit.ZERO;
            }
            return false;
        }
        public override string ToString()
        {
            var result = "";
            foreach (var (digit, position) in this)
            {
                result += string.Format("{0}: {1}; ", position,digit);
            }
            return result.Substring(0, result.Length - 1);
        }
    }
}
