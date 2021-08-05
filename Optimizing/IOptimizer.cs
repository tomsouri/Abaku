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
        Move GetBestMove(IReadOnlyList<Digit> availableDigits, IExtendedBoard board, IFormulaIdentifier formulaIdentifier, IUnsafeEvaluator evaluator, IUnsafeValidator validator);
    }
}
