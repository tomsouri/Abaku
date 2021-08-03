using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReadOnlyListExtensions.Splitting;
using ReadOnlyListExtensions.Sections;
using ReadOnlyListDigitExtensions;
using EnumerableSatisfyingConditionsExtensions;

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
            return digits.GetSectionsContainingIndex(index).AtLeastOneSatisfies(IsFormula);
        }

        public bool ContainsFormulaIncludingIndices(IReadOnlyList<Digit> digits, int index1, int index2)
        {
            return digits.GetSectionsContainingIndices(index1, index2).AtLeastOneSatisfies(IsFormula);
        }
        

        public string GetFormulaString(IReadOnlyList<Digit> digits)
        {
            foreach (int arity in FactorsIdentifier.Arities)
            {
                string result = GetFormulaString(digits, arity);
                if (result!=null) return result;
            }
            return null;
        }

        private string GetFormulaString(IReadOnlyList<Digit> digits, int arity)
        {
            if (arity == 1) return GetUnaryFormulaString(digits);
            else if (arity == 2) return GetBinaryFormulaString(digits);
            else
            {
                foreach (var parts in digits.SplitIntoParts(arity))
                {
                    string result = FactorsIdentifier.GetFormulaString(parts.ToLong());
                    if (result!=null) return result;
                }
            }
            return null;
        }

        private string GetUnaryFormulaString(IReadOnlyList<Digit> digits)
        {
            foreach (var (aFactor, bFactor) in digits.SplitIntoTwoParts())
            {
                string result = FactorsIdentifier.GetFormulaString(aFactor.ToLong(), bFactor.ToLong());
                if (result != null) return result;
            }
            return null;
        }
        private string GetBinaryFormulaString(IReadOnlyList<Digit> digits)
        {
            foreach (var (aFactor, bFactor, cFactor) in digits.SplitIntoThreeParts())
            {
                string result = FactorsIdentifier.GetFormulaString(aFactor.ToLong(), bFactor.ToLong(), cFactor.ToLong());
                if (result != null) return result;
            }
            return null;
        }

        public bool IsFormula(IReadOnlyList<Digit> digits)
        {
            foreach (int arity in FactorsIdentifier.Arities)
            {
                if (IsFormula(digits, arity)) return true;
            }
            return false;
        }

        private bool IsFormula(IReadOnlyList<Digit> digits, int arity)
        {
            if (arity == 1) return IsUnaryFormula(digits);
            else if (arity == 2) return IsBinaryFormula(digits);
            else
            {
                foreach (var parts in digits.SplitIntoParts(arity))
                {
                    if (FactorsIdentifier.IsFormula(parts.ToLong())) return true;
                }
            }
            return false;
        }
        private bool IsUnaryFormula(IReadOnlyList<Digit> digits)
        {
            foreach (var (aFactor,bFactor) in digits.SplitIntoTwoParts())
            {
                if (FactorsIdentifier.IsFormula(aFactor.ToLong(), bFactor.ToLong())) return true;
            }
            return false;
        }
        private bool IsBinaryFormula(IReadOnlyList<Digit> digits)
        {
            foreach (var (aFactor, bFactor, cFactor) in digits.SplitIntoThreeParts())
            {
                if (FactorsIdentifier.IsFormula(aFactor.ToLong(), bFactor.ToLong(), cFactor.ToLong())) return true;
            }
            return false;
        }

    }
}
