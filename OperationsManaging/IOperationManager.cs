using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    public interface IOperationManager
    {
        IFormulaIdentifier FormulaIdentifier { get; }
    }
}
