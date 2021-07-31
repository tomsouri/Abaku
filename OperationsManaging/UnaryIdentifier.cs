using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal abstract class UnaryIdentifier : IFactorsFormulaIdentifier
    {
        public static int Arity => 1;
        
        public bool IsFormula(long aFactor, long bFactor, long cFactor) => false;

        public bool IsFormula(long[] factors)
        {
            if (factors.Length != Arity + 1) return false;
            return IsFormula(factors[0], factors[1]);
        }

        public abstract bool IsFormula(long aFactor, long bFactor);
    }
}
