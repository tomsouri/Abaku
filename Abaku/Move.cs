using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    /// <summary>
    /// Represents one move in a game of Abaku, that is the array of placed stones (digits) with their positions.
    /// Supports also the move evaluation, that is adding the point score of the move.
    /// </summary>
    public struct Move :  IEnumerable<(Digit,Position)>
    {
        public Move((Digit digit, Position position)[] placedStones)
        {
            PlacedStones = placedStones;
            Array.Sort(PlacedStones, (a, b) => a.position.CompareTo(b.position));
            Score = 0;
            IsEvaluated = false;
        }
        public Digit this[Position position]
        {
            get
            {
                foreach (var (digit, pos) in PlacedStones)
                {
                    if (position == pos) return digit;
                }
                throw new InvalidOperationException("The move does not contain the specified position.");
            }
        }
        private readonly (Digit digit, Position position)[] PlacedStones;

        /// <summary>
        /// The score you get playing this move.
        /// </summary>
        public int Score { get; private set; }
        public bool IsEvaluated { get; private set; }

        /// <summary>
        /// Set Score and IsEvaluated fields.
        /// </summary>
        /// <param name="score"></param>
        public void SetEvaluation(int score)
        {
            Score = score;
            IsEvaluated = true;
        }

        public bool ContainsPosition(Position position)
        {
            foreach (var (_, pos) in PlacedStones)
            {
                if (position == pos) return true;
            }
            return false;
        }
        public IReadOnlyList<Position> PositionsSorted { 
            get
            {
                var arr = new Position[PlacedStones.Length];
                for (int i = 0; i < PlacedStones.Length; i++)
                {
                    arr[i] = PlacedStones[i].position;
                }
                return arr;
            } 
        }

        public IEnumerable<Position> GetPositions()
        {
            foreach (var (_, position) in PlacedStones)
            {
                yield return position;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<(Digit,Position)> GetEnumerator()
        {
            foreach (var (digit, position) in PlacedStones)
            {
                yield return (digit,position);
            }
        }
        
        /// <summary>
        /// Determines whether the move places zero to the specified position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>True if the move contains the position and places zero there.</returns>
        public bool ContainsZero(Position position)
        {
            foreach (var (digit,pos) in this)
            {
                if (pos == position) return digit == Digit.ZERO;
            }
            return false;
        }
    }
}
