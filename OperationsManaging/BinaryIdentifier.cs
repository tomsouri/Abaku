using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal delegate bool BinaryOperationDelegate(long aFactor, long bFactor, long result);
    internal sealed class BinaryIdentifier: ISimpleFactorsFormulaIdentifier
    {
        public BinaryIdentifier(string description, BinaryOperationDelegate operationDelegate)
        {
            Description = description;
            OperationDelegate = operationDelegate;
        }
        public static int Arity => 2;
        public int OperatorArity => Arity;
        private BinaryOperationDelegate OperationDelegate { get; }

        public string Description { get; }

        public bool IsFormula(long aFactor, long bFactor) => false;

        public bool IsFormula(long aFactor, long bFactor, long result) => OperationDelegate(aFactor, bFactor, result);

        public bool IsFormula(long[] factors)
        {
            if (factors.Length != Arity + 1) return false;
            return IsFormula(factors[0], factors[1], factors[2]);
        }
    }
}
