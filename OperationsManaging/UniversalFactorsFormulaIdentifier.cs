using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal class UniversalFactorsFormulaIdentifier : IUniversalFactorsFormulaIdentifier
    {
        public UniversalFactorsFormulaIdentifier()
        {
            HasDefaultSetup = true;

            // TODO: add default setup of identifiers.

            throw new NotImplementedException();
        }
        private bool HasDefaultSetup { get; set; }
        private List<int> KnownArities { get; set; }
        public IEnumerable<int> Arities => KnownArities;
        private List<UnaryIdentifier> UnaryIdentifiers { get; }
        private List<BinaryIdentifier> BinaryIdentifiers { get; }

        public void Add(ISimpleFactorsFormulaIdentifier identifier)
        {
            // TODO: check, if it has default setup.
            // if so, clear all identifier lists and add this one.
            // if not, just add this one.

            throw new NotImplementedException();
        }

        public bool IsFormula(long aFactor, long bFactor)
        {
            // call all identifiers with specific arity

            throw new NotImplementedException();
        }

        public bool IsFormula(long aFactor, long bFactor, long cFactor)
        {
            // call all identifiers with specific arity
            throw new NotImplementedException();
        }

        public bool IsFormula(long[] factors)
        {
            // call all identifiers with specific arity
            throw new NotImplementedException();
        }
    }
}
