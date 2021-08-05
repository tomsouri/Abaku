using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using BoardManaging;
using OperationsManaging;
using Evaluation;
using Validation;

namespace Optimizing
{
    public interface IOptimizer
    {
        /// <summary>
        /// Finds the best move in the current situation.
        /// </summary>
        /// <param name="availableDigits">Digits that can be placed in the move.</param>
        /// <param name="board">Board with the current situation.</param>
        /// <param name="formulaIdentifier">Identifier to identify formulas.</param>
        /// <param name="evaluator">Evaluator to get score of the moves.</param>
        /// <param name="validator">Validator to validate the moves.</param>
        /// <returns>The move with the highest score, or null, if there is no valid move.</returns>
        Move? GetBestMove(IReadOnlyList<Digit> availableDigits, IExtendedBoard board, IFormulaIdentifier formulaIdentifier, IUnsafeEvaluator evaluator, IUnsafeValidator validator);
    }
}
