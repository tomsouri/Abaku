using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using BoardManaging;
using OperationsManaging;
using Evaluation;

namespace Optimizing
{
    public interface IOptimizer
    {
        //Move GetBestMove(IReadOnlyList<Digit> availableDigits, IFormulaIdentifier formulaIdentifier,);
        
        //IReadOnlyList<Move> GetBestMoves(I, IBoard board, IFormulaIdentifier formulaIdentifier, MoveValidationDelegate validation, )
    }
}
