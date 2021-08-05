using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OperationsManaging;
using BoardManaging;
using CommonTypes;

namespace Validation
{
    public interface IUnsafeValidator
    {
        /// <summary>
        /// Check whether the move contains all formulas on the adjacent edges.
        /// It expects that the move is positionally valid, that is, does not contain a hole in the middle,
        /// contains only valid positions etc.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>True if it is ok.</returns>
        bool CheckContainedFormulas(Move move, IBoard board, IFormulaIdentifier formulaIdentifier);
    }
}
