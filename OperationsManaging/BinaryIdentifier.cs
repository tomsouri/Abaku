using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal abstract class BinaryIdentifier : ISimpleFactorsFormulaIdentifier
    {
        public static int Arity => 2;

        public abstract string Description { get; }

        public bool IsFormula(long aFactor, long bFactor) => false;

        public abstract bool IsFormula(long aFactor, long bFactor, long cFactor);

        public bool IsFormula(long[] factors)
        {
            if (factors.Length != Arity + 1) return false;
            return IsFormula(factors[0], factors[1], factors[2]);
        }
    }
}
