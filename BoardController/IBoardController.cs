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
        // 
        // ----------------------------------------------------

        /// <summary>
        /// Checks the validity of the move. If it is valid, computes the score and changes the insides of board.
        /// </summary>
        /// <param name="move">The move to enter.</param>
        /// <returns>The score got playing this move.</returns>
        int EnterMove(Move move);

        /// <summary>
        /// Finds all formulas included in the move in current situation and computes their score.
        /// Is is used during the game for displaying all the formulas after applying a move,
        /// included the score you get from the formula.
        /// </summary>
        /// <param name="move">The applyed move.</param>
        /// <returns>List of FormulaRepresentations.</returns>
        IReadOnlyList<FormulaRepresentation> WhichFormulasAreIncludedIn(Move move);

        /// <summary>
        /// Method used by smart agent. Finds the best moves in the current situation.
        /// </summary>
        /// <param name="availableStones"> Stones on hand (digits that can be used).</param>
        /// <returns>The best moves, sorted decreasingly by the score value.</returns>
        IReadOnlyList<Move> GetBestMoves(IReadOnlyList<Digit> availableStones);

        /// <summary>
        /// If a digit is null, it means, that no digit is placed on the position.
        /// TODO: mozna bude vracet immutable 2D array, ktere implementujeme.
        /// </summary>
        /// <returns>The copy of the board.</returns>
        Digit?[,] GetCurrentStateOfBoard();

        /// <summary>
        /// Instead of public constructor we have this public method. Constructor of BoardController is private.
        /// </summary>
        /// <returns>New instance of IBoardController.</returns>
        IBoardController GetBoardController();
        //
        // TODO: operations, evaluation types, types of board.
        // how to setup allowed operations, the type of evaluation and the type of board.
    }
}
