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
            KnownArities = new List<int>() { 1, 2 };
            UnaryIdentifiers = OperationManager.AllOperationsIdentifiers.UnaryIdentifiers.ToList();
            BinaryIdentifiers = OperationManager.AllOperationsIdentifiers.BinaryIdentifiers.ToList();
            OtherIdentifiers = new List<ISimpleFactorsFormulaIdentifier>();
        }
        private bool HasDefaultSetup { get; set; }
        private List<int> KnownArities { get; set; }
        private void AddNewKnownArity(int arity)
        {
            if (KnownArities.Contains(arity))
            {
                // nothing to do
            }
            else
            {
                KnownArities.Add(arity);
            }
        }
        public IEnumerable<int> Arities => KnownArities;
        private List<UnaryIdentifier> UnaryIdentifiers { get; }
        private List<BinaryIdentifier> BinaryIdentifiers { get; }
        private List<ISimpleFactorsFormulaIdentifier> OtherIdentifiers { get; }

        /// <summary>
        /// If 
        /// </summary>
        /// <param name="identifier"></param>
        public void Add(ISimpleFactorsFormulaIdentifier identifier)
        {
            if (HasDefaultSetup)
            {
                KnownArities.Clear();
                UnaryIdentifiers.Clear();
                BinaryIdentifiers.Clear();
                OtherIdentifiers.Clear();
                HasDefaultSetup = false;
            }
            if (identifier is UnaryIdentifier unaryIdentifier)
            {
                UnaryIdentifiers.Add(unaryIdentifier);
            }
            else if (identifier is BinaryIdentifier binaryIdentifier)
            {
                BinaryIdentifiers.Add(binaryIdentifier);
            }
            else
            {
                OtherIdentifiers.Add(identifier);
            }
            AddNewKnownArity(identifier.OperatorArity);
        }

        public bool IsFormula(long aFactor, long bFactor)
        {
            // call all identifiers with specific arity and other identifiers

            throw new NotImplementedException();
        }

        public bool IsFormula(long aFactor, long bFactor, long cFactor)
        {
            // call all identifiers with specific arity and other identifiers
            throw new NotImplementedException();
        }

        public bool IsFormula(long[] factors)
        {
            // call all identifiers with specific arity and other identifiers
            throw new NotImplementedException();
        }
    }
}
