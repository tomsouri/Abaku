using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation
{
    /// <summary>
    /// Is used for evaluating formulas, that is, for a given formula returns a score.
    /// </summary>
    /// <param name="formula">The formula for evaluation.</param>
    /// <returns>Score got from the specified formula.</returns>
    internal delegate int FormulaEvaluationDelegate(IEnumerable<PositionedDigit> formula);
}
