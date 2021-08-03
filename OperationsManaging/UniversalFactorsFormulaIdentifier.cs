using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnumerableCombineExtensions;

namespace OperationsManaging
{
    internal class UniversalFactorsFormulaIdentifier : IUniversalFactorsFormulaIdentifier
    {
        public UniversalFactorsFormulaIdentifier()
        {
            HasDefaultSetup = true;
            UnaryIdentifiers = OperationManager.AllOperationsIdentifiers.UnaryIdentifiers.ToList();
            BinaryIdentifiers = OperationManager.AllOperationsIdentifiers.BinaryIdentifiers.ToList();
            OtherIdentifiers = OperationManager.AllOperationsIdentifiers.OtherIdentifiers.ToList();
            KnownArities = new List<int>();
            foreach (var identifier in (UnaryIdentifiers,BinaryIdentifiers,OtherIdentifiers).Combine())
            {
                AddNewKnownArity(identifier.OperatorArity);
            }
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
        private IEnumerable<ISimpleFactorsFormulaIdentifier> AllIdentifiers
        {
            get => (UnaryIdentifiers, BinaryIdentifiers, OtherIdentifiers).Combine();
        }

        /// <summary>
        /// If this Universal identifier still has default setting, it clears all lists of identifiers.
        /// Anyway it adds the identifier to the corresponding list and adds its arity to the list of
        /// known arities.
        /// </summary>
        /// <param name="identifier">ISimpleFactorsFormulaIdentifier to add.</param>
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
            foreach (var identifier in (UnaryIdentifiers,OtherIdentifiers).Combine())
            {
                if (identifier.IsFormula(aFactor, bFactor)) return true;
            }
            return false;
        }

        public bool IsFormula(long aFactor, long bFactor, long cFactor)
        {
            foreach (var identifier in (BinaryIdentifiers,OtherIdentifiers).Combine())
            {
                if(identifier.IsFormula(aFactor, bFactor, cFactor)) return true;
            }
            return false;
        }

        public bool IsFormula(long[] factors)
        {
            foreach (var identifier in AllIdentifiers)
            {
                if (identifier.IsFormula(factors)) return true; ;
            }
            return false;
        }
    }
}
