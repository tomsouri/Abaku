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
            return GetBestMove(availableDigits, board, formulaIdentifier, evaluator, validator,
                DefaultMaximalDurationMilliseconds);
        }

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
        private EvaluatedMove? GetBestMove(IReadOnlyList<Digit> availableDigits,
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

            //TODO precomputations

            foreach (var move in GetEvaluatedValidMoves(availableDigits,board,formulaIdentifier, evaluator,validator))
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


        private IEnumerable<EvaluatedMove> GetEvaluatedValidMoves(IReadOnlyList<Digit> availableDigits,
                                  IExtendedBoard board,
                                  IFormulaIdentifier formulaIdentifier,
                                  IUnsafeEvaluator evaluator,
                                  IUnsafeValidator validator)
        {
            foreach (var validMove in GetValidMoves(availableDigits,board,formulaIdentifier,validator))
            {
                yield return new EvaluatedMove(validMove, evaluator.EvaluateValidMove(validMove, board, formulaIdentifier));
            }
        }
        
        private IEnumerable<Move> GetValidMoves(IReadOnlyList<Digit> availableDigits,
                                                IExtendedBoard board,
                                                IFormulaIdentifier formulaIdentifier,
                                                IUnsafeValidator validator)
        {
            var auxiliaryArray = new Digit[Math.Max(board.ColumnsCount,board.RowsCount)];
            foreach (var move in GetPosionallyValidMoves(availableDigits,board))
            {
                if (validator.CheckContainedFormulas(move, board, formulaIdentifier, auxiliaryArray))
                {
                    yield return move;
                }
            }
        }
        private IEnumerable<Move> GetPosionallyValidMoves(IReadOnlyList<Digit> availableDigits, IExtendedBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
