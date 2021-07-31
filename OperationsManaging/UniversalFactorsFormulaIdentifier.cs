using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal class UniversalFactorsFormulaIdentifier : IUniversalFactorsFormulaIdentifier
    {
        private List<int> KnownArities { get; set; }
        public IEnumerable<int> Arities => KnownArities;
        private List<UnaryIdentifier> UnaryIdentifiers { get; }
        private List<BinaryIdentifier> BinaryIdentifiers { get; }

        public void Add(IFactorsFormulaIdentifier identifier)
        {
            throw new NotImplementedException();
        }

        public bool IsFormula(long aFactor, long bFactor)
        {
            throw new NotImplementedException();
        }

        public bool IsFormula(long aFactor, long bFactor, long cFactor)
        {
            throw new NotImplementedException();
        }

        public bool IsFormula(long[] factors)
        {
            throw new NotImplementedException();
        }
    }
}
