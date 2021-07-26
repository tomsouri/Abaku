using CommonTypes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardController
{
    namespace Validator
    {
        class Validator : IValidator
        {
            /// <summary>
            /// Is the move valid in the current context.
            /// </summary>
            /// <param name="move"></param>
            /// <param name="board"></param>
            /// <param name="formulaIdentifier"></param>
            /// <returns>True if it is valid.</returns>
            public bool IsValid(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
            {
                if (!CheckPositionsValidity(move, board)) return false;

                return CheckFormulas(move, board, formulaIdentifier);
            }

            /// <summary>
            /// Check validity of the positions used in the move in the current context.
            /// </summary>
            /// <param name="move">The move containing the positions.</param>
            /// <param name="board">The context.</param>
            /// <returns>True if the positions are valid.</returns>
            private bool CheckPositionsValidity(Move move, IBoard board)
            {
                if (!move.ArePositionsPairwiseDistinct()) return false;
                if (!move.AllPositionsInSameRowOrColumn()) return false;
                if (!AllPositionsEmpty(move, board)) return false;
                if (!NoGapBetweenFirstAndLastPosition(move, board)) return false;
                if (!OccupiesTheRightPositions(move, board)) return false;
                return true;
            }

            /// <summary>
            /// Check emptiness of the positions on the board.
            /// </summary>
            /// <param name="move">Move struct containing the positions.</param>
            /// <param name="board">The current board.</param>
            /// <returns>True if they are empty.</returns>
            private bool AllPositionsEmpty(Move move, IBoard board)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Check, whether there is no empty cell between the first and the last placed stone.
            /// </summary>
            /// <param name="move"></param>
            /// <param name="board"></param>
            /// <returns>True if it is OK.</returns>
            private bool NoGapBetweenFirstAndLastPosition(Move move, IBoard board)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Check whether the right positions are occupied, that is,
            /// in the first move, the Starting position must be occupied,
            /// in the other moves, at least one placed stones must be placed 
            /// to a position adjacent to an already occupied position.
            /// </summary>
            /// <param name="move"></param>
            /// <param name="board"></param>
            /// <returns></returns>
            private bool OccupiesTheRightPositions(Move move, IBoard board)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Check whether the move contains all formulas on the adjacent edges.
            /// </summary>
            /// <param name="move"></param>
            /// <param name="board"></param>
            /// <param name="formulaIdentifier"></param>
            /// <returns>True if it is ok.</returns>
            private bool CheckFormulas(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
            {
                throw new NotImplementedException();
            }

        }
        internal static class MoveExtensions
        {
            public static bool ArePositionsPairwiseDistinct(this Move move)
            {
                throw new NotImplementedException();
            }
            public static bool AllPositionsInSameRowOrColumn(this Move move)
            {
                throw new NotImplementedException();
            }
        }
    }

}
