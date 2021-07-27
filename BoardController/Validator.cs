using CommonTypes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardController.Validator.IEnumerablePositionExtensions;

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
                if (!CheckPositionsValidity(move.GetPositions(), board)) return false;

                return CheckFormulas(move, board, formulaIdentifier);
            }

            /// <summary>
            /// Check validity of the positions used in the move in the current context.
            /// </summary>
            /// <param name="positions">The move containing the positions.</param>
            /// <param name="board">The context.</param>
            /// <returns>True if the positions are valid.</returns>
            private bool CheckPositionsValidity(IEnumerable<Position> positions, IBoard board)
            {
                
                if (!positions.ArePositionsPairwiseDistinct()) return false;
                if (!positions.AllPositionsInSameRowOrColumn()) return false;
                if (!AllPositionsEmpty(positions, board)) return false;
                if (!NoGapBetweenFirstAndLastPosition(positions, board)) return false;
                if (!OccupiesTheRightPositions(positions, board)) return false;
                return true;
            }

            /// <summary>
            /// Check emptiness of the positions on the board.
            /// </summary>
            /// <param name="positions"></param>
            /// <param name="board">The current board.</param>
            /// <returns>True if they are empty.</returns>
            private bool AllPositionsEmpty(IEnumerable<Position> positions, IBoard board)
            {
                return positions.All(board.IsPositionEmpty);
            }

            /// <summary>
            /// Check, whether there is no empty cell between the first and the last placed stone.
            /// </summary>
            /// <param name="positions"></param>
            /// <param name="board"></param>
            /// <returns>True if it is OK.</returns>
            private bool NoGapBetweenFirstAndLastPosition(IEnumerable<Position> positions, IBoard board)
            {

                var (min, max) = positions.FindMinAndMax();
                foreach (var position in Position.GetPositionsBetween(min,max))
                {
                    if (board.IsPositionEmpty(position) && !positions.Contains(position)) return false;
                }
                return true;
            }



            /// <summary>
            /// Check whether the right positions are occupied, that is,
            /// in the first move, the Starting position must be occupied,
            /// in the other moves, at least one placed stones must be placed 
            /// to a position adjacent to an already occupied position.
            /// </summary>
            /// <param name="positions"></param>
            /// <param name="board"></param>
            /// <returns></returns>
            private bool OccupiesTheRightPositions(IEnumerable<Position> positions, IBoard board)
            {
                if (board.IsEmpty())
                {
                    // first move
                    return positions.AtLeastOneSatisfies(board.IsStartingPosition);
                }
                else
                {
                    // standard move
                    return positions.AtLeastOneSatisfies(board.IsAdjacentToOccupiedPosition);
                }
            }

            /// <summary>
            /// Check whether the move contains all formulas on the adjacent edges.
            /// It expects that the move satisfies all conditions, which are checked in CheckPositionsValidity.
            /// </summary>
            /// <param name="move"></param>
            /// <param name="board"></param>
            /// <param name="formulaIdentifier"></param>
            /// <returns>True if it is ok.</returns>
            private bool CheckFormulas(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
            {
                if (ContainsFormulaFromFirstStoneToLast(move, board, formulaIdentifier))
                {
                    return CheckAdjacentOccupiedPositions(move, board, formulaIdentifier);
                }
                return false;

            }
            private bool ContainsFormulaFromFirstStoneToLast(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
            {
                var positions = move.GetPositions();
                var (min, max) = positions.FindMinAndMax();
                var (start, end) = board.GetLongestFilledSectionBounds(ignoreVacancy: positions);
                var digits = board.GetSectionAfterApplyingMove(start, end, move);

                // To compute the indices of min and max position in the array of digit we subtract start position
                return formulaIdentifier.ContainsFormulaIncludingIndices(digits, min - start, max - start);
            }
            private bool CheckAdjacentOccupiedPositions(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
            {
                bool isAnyOccupiedPositionUsedInFormula = false;
                if (board.IsEmpty()) isAnyOccupiedPositionUsedInFormula = true;



                return isAnyOccupiedPositionUsedInFormula;
                throw new NotImplementedException();
            }

        }

    }

}
