using System;
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
    public struct Move
    {
        (Digit, Position)[] PlacedStones;

        /// <summary>
        /// The score you get playing this move.
        /// </summary>
        public int Score { get; private set; }
        public bool IsEvaluated { get; private set; }
        public Move((Digit,Position)[] placedStones)
        {
            PlacedStones = placedStones;
            Score = 0;
            IsEvaluated = false;
        }
    }
}
