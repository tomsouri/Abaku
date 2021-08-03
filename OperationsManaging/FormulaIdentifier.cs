using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReadOnlyListExtensions;
using ReadOnlyListDigitExtensions;

namespace OperationsManaging
{
    internal class FormulaIdentifier : IFormulaIdentifier
    {
        /// <summary>
        /// Identifier used to identify, whether a tuple (or array) of factors
        /// represents a valid formula or not, and to get the formula string representation.
        /// </summary>
        private IUniversalFactorsFormulaIdentifier FactorsIdentifier { get; }
        public FormulaIdentifier(IUniversalFactorsFormulaIdentifier factorsIdentifier)
        {
            FactorsIdentifier = factorsIdentifier;
        }
        public bool ContainsFormulaIncludingIndex(IReadOnlyList<Digit> digits, int index)
        {
            throw new NotImplementedException();
        }

        public bool ContainsFormulaIncludingIndices(IReadOnlyList<Digit> digits, int index1, int index2)
        {
            throw new NotImplementedException();
        }

        public string GetFormulaString(IReadOnlyList<Digit> digits)
        {
            throw new NotImplementedException();
        }

        private string GetFormulaString(IReadOnlyList<Digit> digits, int arity)
        {
            throw new NotImplementedException();
        }


        public bool IsFormula(IReadOnlyList<Digit> digits)
        {
            throw new NotImplementedException();
        }

        private bool IsFormula(IReadOnlyList<Digit> digits, int arity)
        {
            throw new NotImplementedException();
        }

    }
}
