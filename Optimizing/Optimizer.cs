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
            foreach (var validMove in GetValidMoves(availableDigits,board,formulaIdentifier,validator))
            {
                int score = evaluator.EvaluateValidMove(validMove,board,formulaIdentifier);

            }
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
