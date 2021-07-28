using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using OperationsManaging;
using BoardManaging;

namespace Validation
{
    public interface IValidator
    {
        /// <summary>
        /// Determines, whether a move is a valid move in the current situation,
        /// that is with the current board and the concrete Formula Identifier.
        /// </summary>
        /// <param name="move">Move to be validated.</param>
        /// <param name="board">Current board.</param>
        /// <param name="formulaIdentifier">Formula Identifier to identify arrays of digits as formulas.</param>
        /// <returns>True if the move is valid.</returns>
        bool IsValid(Move move, IBoard board, IFormulaIdentifier formulaIdentifier);
    }
}
