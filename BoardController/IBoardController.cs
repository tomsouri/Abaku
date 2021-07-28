using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace BoardController
{
    public interface IBoardController
    {
        // ----------------------------------------------------
        // Not going to be in the interface:
        // bool IsValid(Move move);
        // int Evaluate(Move move);
        // IReadOnlyList<Formula> GetAllFormulas(Move move);
        // ----------------------------------------------------

        /// <summary>
        /// Checks the validity of the move. If it is valid, computes the score and changes the insides of board.
        /// </summary>
        /// <param name="move">The move to enter.</param>
        /// <returns>The score got playing this move.</returns>
        int EnterMove(Move move);

        /// <summary>
        /// Method used by smart agent. Finds the best moves in the current situation.
        /// </summary>
        /// <param name="availableStones"> Stones on hand (digits that can be used).</param>
        /// <returns>The best moves, sorted decreasingly by the score value.</returns>
        IReadOnlyList<Move> GetBestMoves(IReadOnlyList<Digit> availableStones);
        
        //
        // TODO: operations, evaluation types, types of board.
        // how to setup allowed operations, the type of evaluation and the type of board.
    }
}
