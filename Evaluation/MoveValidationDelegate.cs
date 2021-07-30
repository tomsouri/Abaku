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
    public delegate bool MoveValidationDelegate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier);
}
