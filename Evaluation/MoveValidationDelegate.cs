using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using OperationsManaging;
using BoardManaging;

namespace Evaluation
{
    /// <summary>
    /// Determines, whether the given move is valid in the given context, or not.
    /// </summary>
    /// <param name="move">The move to validate.</param>
    /// <param name="board">The context, content of the board.</param>
    /// <param name="formulaIdentifier">The context, represents allowed formulas.</param>
    /// <returns>True if the move is valid.</returns>
    public delegate bool MoveValidationDelegate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier);
}
