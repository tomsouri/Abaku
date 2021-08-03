using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal class FormulaIdentifier : IFormulaIdentifier
    {
        private IUniversalFactorsFormulaIdentifier FactorsIdentifier { get; }
        public FormulaIdentifier(IUniversalFactorsFormulaIdentifier factorsIdentifier)
        {
            FactorsIdentifier = factorsIdentifier;
        }
        public bool ContainsFormulaIncludingIndices(IReadOnlyList<Digit> digits, int index)
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

        public bool IsFormula(IReadOnlyList<Digit> digits)
        {
            throw new NotImplementedException();
        }
    }
}
