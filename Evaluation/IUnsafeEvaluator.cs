using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using BoardManaging;
using OperationsManaging;

namespace Evaluation
{
    public interface IUnsafeEvaluator
    {
        /// <summary>
        /// Evaluates the valid move in the current situation.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>The score you get after applying the move.</returns>
        int EvaluateValidMove(Move move, IBoard board, IFormulaIdentifier formulaIdentifier);
    }
}
