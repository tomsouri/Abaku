﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using BoardManaging;
using OperationsManaging;
using EnumerablePositionExtensions;
using EnumerableExtensions;


namespace Validation
{
    /// <summary>
    /// Implements singleton pattern.
    /// </summary>
    public class Validator : IValidator, IUnsafeValidator
    {
        /// <summary>
        /// The only instance.
        /// </summary>
        private static readonly Validator Singleton = new();

        /// <summary>
        /// Instance getter.
        /// </summary>
        public static IValidator Instance {get => Singleton;}

        private Validator() { }


        /// <summary>
        /// Is the move valid in the current context?
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>True if it is valid.</returns>
        bool IValidator.IsValid(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
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
            return positions.ArePairwiseDistinct() &&
                positions.AllPositionsInSameRowOrColumn() &&
                AllPositionsEmpty(positions, board) &&
                NoGapBetweenFirstAndLastPosition(positions, board) &&
                OccupiesTheRightPositions(positions, board);
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
        private static bool CheckFormulas(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, Digit[] auxiliaryArray = null)
        {
            if (move.GetPositions().Count() == 1)
            {   // the move placed only one digit. No need to check, whether they are all contained in one formula.
                return CheckAdjacentOccupiedPositions(move, board, formulaIdentifier, auxiliaryArray);
            }
            return ContainsFormulaFromFirstStoneToLast(move, board, formulaIdentifier, auxiliaryArray) &&
                CheckAdjacentOccupiedPositions(move, board, formulaIdentifier, auxiliaryArray);

        }

        /// <summary>
        /// Check, whether the board after applying the move contains a formula including all placed stones
        /// (that is, contains a formula from the first to the last stone).
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>True if it contains the formula.</returns>
        private static bool ContainsFormulaFromFirstStoneToLast(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, Digit[] auxiliaryArray)
        {
            var boardAfterMove = new BoardAfterMove(board, move);

            var positions = move.GetPositions();
            var (min, max) = positions.FindMinAndMax();
            return boardAfterMove.ContainsFormulaIncludingPositions(min, max, formulaIdentifier, auxiliaryArray);
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
        private static bool CheckAdjacentOccupiedPositions(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, Digit[] auxiliaryArray)
        {
            // If it is not the first move, at least one adjacent position must be used in some formula.
            bool isSomeOccupiedPositionUsedInFormula = false;
            if (board.IsEmpty())
            { // it is the first move
                isSomeOccupiedPositionUsedInFormula = true;
            }

            var positions = move.GetPositions();
            var (min, max) = positions.FindMinAndMax();

            var boardAfterMove = new BoardAfterMove(board, move);

            foreach (var position in positions)
            {
                foreach (var adjacent in board.GetAdjacentOccupiedPositions(position))
                {
                    if ((min <= adjacent) && (adjacent <= max))
                    {
                        // The adjacent position is between the first and last placed stone, that is,
                        // it is already included in the base formula.
                        isSomeOccupiedPositionUsedInFormula = true;
                    }
                    else if (boardAfterMove.ContainsZero(position) || board.ContainsZero(adjacent))
                    {
                        // nothing to do, the adj position neighbors through zero
                    }
                    else if (boardAfterMove.ContainsFormulaIncludingPositions(position, adjacent, formulaIdentifier, auxiliaryArray))
                    {
                        isSomeOccupiedPositionUsedInFormula = true;
                    }
                    else return false;
                }
            }
            if (!isSomeOccupiedPositionUsedInFormula)
            {
                // Check again positions adjacent through zero, whether they contain some formula.
                foreach (var position in positions)
                {
                    foreach (var adjacent in board.GetAdjacentOccupiedPositions(position))
                    {
                        if (boardAfterMove.ContainsZero(position) || board.ContainsZero(adjacent))
                        {
                            if (boardAfterMove.ContainsFormulaIncludingPositions(position, adjacent, formulaIdentifier, auxiliaryArray))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return isSomeOccupiedPositionUsedInFormula;
        }

        bool IUnsafeValidator.CheckContainedFormulas(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, Digit[] auxiliaryArray)
        {
            return CheckFormulas(move, board, formulaIdentifier, auxiliaryArray);
        }

        /// <summary>
        /// Simplifying object for accessing content of a board after applying a move.
        /// </summary>
        private struct BoardAfterMove
        {
            private Move Move { get; }
            private IBoard Board { get; }

            /// <summary>
            /// Does not check validity of the move, i.e. allows the move to contain a position,
            /// which is not empty on the board.
            /// </summary>
            /// <param name="board"></param>
            /// <param name="move"></param>
            public BoardAfterMove(IBoard board, Move move)
            {
                this.Move = move;
                this.Board = board;
            }
            /// <summary>
            /// Determines, whether board or move contains zero digit on the given position.
            /// </summary>
            /// <param name="position">Given position to check.</param>
            /// <returns>True if at least one of them contains zero digit.</returns>
            public bool ContainsZero(Position position)
            {
                return Board.ContainsZero(position) || Move.ContainsZero(position);
            }

            /// <summary>
            /// Determines, whether there exists a formula which contains both positions included1 and included2.
            /// </summary>
            /// <param name="included1">Position, which has to be contained in the formula.</param>
            /// <param name="included2">The other position. It must be in the same row or column as included1.</param>
            /// <param name="formulaIdentifier"></param>
            /// <returns>True if there is such a formula.</returns>
            public bool ContainsFormulaIncludingPositions(Position included1, Position included2, IFormulaIdentifier formulaIdentifier, Digit[] auxiliaryArray)
            {
                var (start, end) = Board.GetLongestFilledSectionBounds(ToBeContained:(included1, included2), ignoreVacancy:Move.GetPositions());
                var digits = Board.GetSectionAfterApplyingMove(start, end, Move, auxiliaryArray);
                var index1 = included1 - start;
                var index2 = included2 - start;
                return formulaIdentifier.ContainsFormulaIncludingIndices(digits, index1, index2);
            }
        }
    }
}
