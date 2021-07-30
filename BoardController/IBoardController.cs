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
        /// <summary>
        /// Checks, whether the given move is valid in the current situation or not.
        /// </summary>
        /// <param name="move">The move to check.</param>
        /// <returns>True if the move is valid.</returns>
        bool IsValid(Move move);

        /// <summary>
        /// If the move is valid, evaluates it and returns score you get for playing the move.
        /// If the move is not valid, returns score you get (that is usually some negative score).
        /// </summary>
        /// <param name="move">The move to evaluate.</param>
        /// <returns>Score you get by applying the given move.</returns>
        int Evaluate(Move move);

        /// <summary>
        /// Finds all formulas included in the move in current situation and computes their score.
        /// Is is used during the game for displaying all the formulas after applying a move,
        /// included the score you get from the formula.
        /// </summary>
        /// <param name="move">The applyed move.</param>
        /// <returns>List of FormulaRepresentations.</returns>
        IReadOnlyList<FormulaRepresentation> AllFormulasIncludedIn(Move move);

        /// <summary>
        /// Checks the validity of the move. If it is valid, computes the score and changes the insides of board.
        /// If the move is not valid, does not change the insides of the board.
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

        /// <summary>
        /// Enters move WITHOUT checking its validity. Do not use in normal game!
        /// </summary>
        /// <param name="move"></param>
        void EnterMoveUnsafe(Move move);


        


        /// <summary>
        /// If a digit is null, it means, that no digit is placed on the position.
        /// TODO: mozna bude vracet immutable 2D array, ktere implementujeme.
        /// </summary>
        /// <returns>The copy of the board.</returns>
        Digit?[,] GetBoardContent();

        //
        // TODO: operations, evaluation types, types of board.
        // how to setup allowed operations, the type of evaluation and the type of board.
    }
}
