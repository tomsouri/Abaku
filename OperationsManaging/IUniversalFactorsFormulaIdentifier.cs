using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal interface IUniversalFactorsFormulaIdentifier : IFactorsFormulaIdentifier
    {
        IEnumerable<int> Arities { get; }
        void Add(ISimpleFactorsFormulaIdentifier identifier);

    }
}
