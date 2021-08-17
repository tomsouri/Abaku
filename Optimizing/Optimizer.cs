using BoardManaging;
using CommonTypes;
using Evaluation;
using OperationsManaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation;

using System.Diagnostics;

namespace Optimizing
{
    public class Optimizer : IOptimizer
    {


        /// <summary>
        /// Maximal duration of one computation of the best move.
        /// </summary>
        private static long DefaultMaximalDurationMilliseconds => 5000;

        /// <summary>
        /// Finds the best move in the current situation, 
        /// with the default time limit.
        /// </summary>
        /// <param name="availableDigits">Digits that can be placed in the move.</param>
        /// <param name="board">Board with the current situation.</param>
        /// <param name="formulaIdentifier">Identifier to identify formulas.</param>
        /// <param name="evaluator">Evaluator to get score of the moves.</param>
        /// <param name="validator">Validator to validate the moves.</param>
        /// <returns>The move with the highest score, or null, if there is no valid move.</returns>
        public EvaluatedMove? GetBestMove(IReadOnlyList<Digit> availableDigits,
                                 IExtendedBoard board,
                                 IFormulaIdentifier formulaIdentifier,
                                 IUnsafeEvaluator evaluator,
                                 IUnsafeValidator validator)
        {
            return BestMovesFinder.GetBestMove(availableDigits, board, formulaIdentifier, evaluator, validator,
                DefaultMaximalDurationMilliseconds);
        }

        private static class BestMovesFinder
        {
            /// <summary>
            /// Finds the best move in the current situation,
            /// with the given time limitation.
            /// </summary>
            /// <param name="availableDigits">Digits that can be placed in the move.</param>
            /// <param name="board">Board with the current situation.</param>
            /// <param name="formulaIdentifier">Identifier to identify formulas.</param>
            /// <param name="evaluator">Evaluator to get score of the moves.</param>
            /// <param name="validator">Validator to validate the moves.</param>
            /// <param name="maxDurationMilliseconds">Maximal duration of the computation.</param>
            /// <returns></returns>
            public static EvaluatedMove? GetBestMove(IReadOnlyList<Digit> availableDigits,
                                      IExtendedBoard board,
                                      IFormulaIdentifier formulaIdentifier,
                                      IUnsafeEvaluator evaluator,
                                      IUnsafeValidator validator,
                                      long maxDurationMilliseconds)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                EvaluatedMove? bestMove = null;
                int bestScore = 0;

                var evaluatedMoves = GetEvaluatedMoves(availableDigits, board, formulaIdentifier, evaluator, validator);

                foreach (var move in evaluatedMoves)
                {
                    if (move.Score >= bestScore)
                    {
                        bestMove = move;
                        bestScore = move.Score;
                    }
                    if (stopwatch.ElapsedMilliseconds > maxDurationMilliseconds)
                    {
                        break;
                    }
                }
                return bestMove;
            }
            private static IEnumerable<EvaluatedMove> GetEvaluatedMoves(IReadOnlyList<Digit> availableDigits,
                          IExtendedBoard board,
                          IFormulaIdentifier formulaIdentifier,
                          IUnsafeEvaluator evaluator,
                          IUnsafeValidator validator)
            {
                foreach (var validMove in GetValidMoves(availableDigits, board, formulaIdentifier, validator))
                {
                    yield return new EvaluatedMove(validMove, evaluator.EvaluateValidMove(validMove, board, formulaIdentifier));
                }
            }

            private static IEnumerable<Move> GetValidMoves(IReadOnlyList<Digit> availableDigits,
                                                    IExtendedBoard board,
                                                    IFormulaIdentifier formulaIdentifier,
                                                    IUnsafeValidator validator)
            {
                var auxiliaryArray = new Digit[Math.Max(board.ColumnsCount, board.RowsCount)];
                foreach (var move in GetPositionallyValidMoves(availableDigits, board))
                {
                    if (validator.CheckContainedFormulas(move, board, formulaIdentifier, auxiliaryArray))
                    {
                        yield return move;
                    }
                }
            }
            private static IEnumerable<Move> GetPositionallyValidMoves(IReadOnlyList<Digit> availableDigits, IExtendedBoard board)
            {
                var OptBoard = new OptimizerBoard(board, availableDigits.Count);

                foreach (Digit[] sequence in DigitsSequenceGenerator.GetAllSequences(availableDigits))
                {
                    foreach (Direction direction in Direction.SimpleDirections)
                    {
                        foreach (Position position in OptBoard.GetPositionsSuitableForDigitsCount(direction,sequence.Length))
                        {
                            yield return new Move(sequence, OptBoard.GetEmptyPositionsBeyond(position, direction, sequence.Length));
                        }
                    }
                }
            }
        }

        private static class DigitsSequenceGenerator
        {
            public static IEnumerable<Digit[]> GetAllSequences(IReadOnlyList<Digit> availableDigits)
            {
                var l = new Stack<int>();
                var p = l.ToArray();
                
                
                // TODO
                // pomoci rekurzivniho algoritmu vytvori vsechny mozne posloupnosti cifer,
                // ktere muzeme polozit, mame-li na ruce dane cifry
                // - kazdou posloupnost chceme vratit prave jednou
                throw new NotImplementedException();
            }
        }
    }
    internal static class SortedEnumerableExtensions
    {
        /// <summary>
        /// For a sorted enumerable returns only distinct items, that is, ignores repeted items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetDistinct<T>(this IEnumerable<T> enumerable) where T: IEquatable<T>
        {
            bool isFirst = true;

            // assignment only for csc to allow equality comparison in the foreach loop
            T last = default;
            foreach (var item in enumerable)
            {
                if (isFirst)
                {
                    yield return item;
                    isFirst = false;
                }
                else
                {
                    if (!item.Equals(last)) yield return item;
                }

                last = item;
            }
        }
    }
}
