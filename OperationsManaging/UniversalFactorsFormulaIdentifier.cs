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
            UnaryIdentifiers = OperationManager.AllOperationsIdentifiers.UnaryIdentifiers.ToList();
            BinaryIdentifiers = OperationManager.AllOperationsIdentifiers.BinaryIdentifiers.ToList();
            OtherIdentifiers = OperationManager.AllOperationsIdentifiers.OtherIdentifiers.ToList();
            KnownArities = new List<int>();
            foreach (var identifier in AllIdentifiers)
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
        private IEnumerable<ISimpleFactorsFormulaIdentifier> AllIdentifiers { 
            get
            {
                foreach (var identifier in UnaryIdentifiers)
                {
                    yield return identifier;
                }
                foreach (var identifier in BinaryIdentifiers)
                {
                    yield return identifier;
                }
                foreach (var identifier in OtherIdentifiers)
                {
                    yield return identifier;
                }
            } 
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
