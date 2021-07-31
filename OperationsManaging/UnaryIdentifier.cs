using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    /*internal abstract class UnaryIdentifier : ISimpleFactorsFormulaIdentifier
    {
        public static int Arity => 1;
        public int OperatorArity => Arity;

        public abstract string Description { get; }

        public bool IsFormula(long aFactor, long bFactor, long cFactor) => false;

        public bool IsFormula(long[] factors)
        {
            if (factors.Length != Arity + 1) return false;
            return IsFormula(factors[0], factors[1]);
        }

        public abstract bool IsFormula(long aFactor, long bFactor);
    }*/

    internal delegate bool UnaryOperationDelegate(long factor, long result);

    internal sealed class UnaryIdentifier : ISimpleFactorsFormulaIdentifier
    {
        public UnaryIdentifier(string description, UnaryOperationDelegate operationDelegate)
        {
            Description = description;
            OperationDelegate = operationDelegate;
        }
        public static int Arity => 1;
        public int OperatorArity => Arity;

        private UnaryOperationDelegate OperationDelegate { get; }
        public string Description { get; }

        public bool IsFormula(long aFactor, long bFactor, long cFactor) => false;

        public bool IsFormula(long[] factors)
        {
            if (factors.Length != Arity + 1) return false;
            return IsFormula(factors[0], factors[1]);
        }

        public bool IsFormula(long factor, long result) => OperationDelegate(factor,result);
    }

}
