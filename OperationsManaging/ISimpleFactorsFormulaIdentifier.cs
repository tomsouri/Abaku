using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal interface ISimpleFactorsFormulaIdentifier : IFactorsFormulaIdentifier
    {
        int Arity { get; }
    }
}
