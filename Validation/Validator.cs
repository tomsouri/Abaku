using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using BoardManaging;
using OperationsManaging;


namespace Validation
{
    internal class Validator : IValidator
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
        private static bool CheckPositionsValidity(IEnumerable<Position> positions, IBoard board)
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
        private static bool AllPositionsEmpty(IEnumerable<Position> positions, IBoard board)
        {
            return positions.All(board.IsPositionEmpty);
        }

        /// <summary>
        /// Check, whether there is no empty cell between the first and the last placed stone.
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="board"></param>
        /// <returns>True if it is OK.</returns>
        private static bool NoGapBetweenFirstAndLastPosition(IEnumerable<Position> positions, IBoard board)
        {

            var (min, max) = positions.FindMinAndMax();
            foreach (var position in Position.GetPositionsBetween(min, max))
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
        private static bool OccupiesTheRightPositions(IEnumerable<Position> positions, IBoard board)
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
        private static bool CheckFormulas(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            if (ContainsFormulaFromFirstStoneToLast(move, board, formulaIdentifier))
            {
                return CheckAdjacentOccupiedPositions(move, board, formulaIdentifier);
            }
            return false;

        }

        /// <summary>
        /// Check, whether the board after applying the move contains a formula including all placed stones
        /// (that is, contains a formula from the first to the last stone).
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>True if it contains the formula.</returns>
        private static bool ContainsFormulaFromFirstStoneToLast(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            var boardAfterMove = board.GetBoardAfterHypotheticalMove(move);

            var positions = move.GetPositions();
            var (min, max) = positions.FindMinAndMax();
            return boardAfterMove.ContainsFormulaIncludingPositions(min, max, formulaIdentifier);
        }

        /// <summary>
        /// Every adjacent (and already occupied) position must fulfill some concrete condition.
        /// That is, it contains zero or neighbors with zero, or contains a formula together with
        /// the adjacent placed stone.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <returns></returns>
        private static bool CheckAdjacentOccupiedPositions(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            // If it is not the first move, at least one adjacent position must be used in some formula.
            bool isAnyOccupiedPositionUsedInFormula = false;
            if (board.IsEmpty())
            { // it is the first move
                isAnyOccupiedPositionUsedInFormula = true;
            }

            var positions = move.GetPositions();
            var (min, max) = positions.FindMinAndMax();

            var boardAfterMove = board.GetBoardAfterHypotheticalMove(move);

            foreach (var position in positions)
            {
                foreach (var adjacent in board.GetAdjacentOccupiedPositions(position))
                {
                    if ((min <= adjacent) && (adjacent <= max))
                    {
                        // The adjacent position is between the first and last placed stone, that is,
                        // it is already included in the base formula.
                        isAnyOccupiedPositionUsedInFormula = true;
                    }
                    else
                    {
                        if (boardAfterMove.ContainsFormulaIncludingPositions(position, adjacent, formulaIdentifier))
                        {
                            // It is ok, the adjacent position is included in a formula.
                            isAnyOccupiedPositionUsedInFormula = true;
                        }
                        else
                        {
                            // it has to be adjacent through zero number.
                            if (!boardAfterMove.ContainsZero(position) && !board.ContainsZero(adjacent))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return isAnyOccupiedPositionUsedInFormula;
        }

    }
}
