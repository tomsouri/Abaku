using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal delegate bool UnaryOperationDelegate(long factor, long result);

    internal sealed class UnaryIdentifier : ISimpleFactorsFormulaIdentifier
    {
        public UnaryIdentifier(string description, UnaryOperationDelegate operationDelegate, string operationString)
        {
            Description = description;
            OperationDelegate = operationDelegate;
            OperationString = operationString;
        }
        public static int Arity => 1;
        public int OperatorArity => Arity;

        private UnaryOperationDelegate OperationDelegate { get; }
        private string OperationString { get; }
        private static string EqualityString => "=";
        public string Description { get; }

        public bool IsFormula(long aFactor, long bFactor, long cFactor) => false;

        public bool IsFormula(long[] factors)
        {
            if (factors.Length != Arity + 1) return false;
            return IsFormula(factors[0], factors[1]);
        }

        public bool IsFormula(long factor, long result) => OperationDelegate(factor,result);

        public string GetFormulaString(long factor, long result)
        {
            if (!IsFormula(factor, result)) return null;
            return factor.ToString() + OperationString + EqualityString + result.ToString();
        }

        public string GetFormulaString(long aFactor, long bFactor, long result)
        {
            return null;
        }

        public string GetFormulaString(long[] factors)
        {
            if (factors.Length != Arity + 1) return null;
            return GetFormulaString(factors[0], factors[1]);
        }
    }

}
