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
        private static long DefaultMaximalDurationMilliseconds => 5000;
        public Move? GetBestMove(IReadOnlyList<Digit> availableDigits,
                                 IExtendedBoard board,
                                 IFormulaIdentifier formulaIdentifier,
                                 IUnsafeEvaluator evaluator,
                                 IUnsafeValidator validator)
        {
            return GetBestMove(availableDigits, board, formulaIdentifier, evaluator, validator, DefaultMaximalDurationMilliseconds);
        }
        private Move? GetBestMove(IReadOnlyList<Digit> availableDigits,
                                  IExtendedBoard board,
                                  IFormulaIdentifier formulaIdentifier,
                                  IUnsafeEvaluator evaluator,
                                  IUnsafeValidator validator,
                                  long maxDurationMilliseconds)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Move? bestMove = null;
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
        private IEnumerable<Move> GetEvaluatedValidMoves(IReadOnlyList<Digit> availableDigits,
                                  IExtendedBoard board,
                                  IFormulaIdentifier formulaIdentifier,
                                  IUnsafeEvaluator evaluator,
                                  IUnsafeValidator validator)
        {
            foreach (var validMove in GetValidMoves(availableDigits,board,formulaIdentifier,validator))
            {
                validMove.SetEvaluation(evaluator.EvaluateValidMove(validMove, board, formulaIdentifier));
                yield return validMove;
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
