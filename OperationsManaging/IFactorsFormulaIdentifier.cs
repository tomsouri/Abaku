using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal interface IFactorsFormulaIdentifier
    {
        bool IsFormula(long aFactor, long bFactor);
        bool IsFormula(long aFactor, long bFactor, long cFactor);
        bool IsFormula(long[] factors);
    }
}
