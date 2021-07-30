using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace OperationsManaging
{
    public interface IOperationManager
    {
        IFormulaIdentifier FormulaIdentifier { get; }
        IReadOnlyList<ISetupTool> GetOperationSetTools();
    }
}
