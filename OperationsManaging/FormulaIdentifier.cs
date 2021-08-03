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
            return FactorsIdentifier.Arities.
                GetResults(arity => GetFormulaString(digits, arity)).
                FindFirstNotNull();
        }

        private string GetFormulaString(IReadOnlyList<Digit> digits, int arity)
        {
            if (arity == 1) return GetUnaryFormulaString(digits);
            else if (arity == 2) return GetBinaryFormulaString(digits);
            else return GetOtherFormulaString(digits, arity);
        }

        private string GetUnaryFormulaString(IReadOnlyList<Digit> digits)
        {
            
            return digits.SplitIntoTwoParts().
                GetResults(digitsTuple => digitsTuple.ToLong()).
                GetResults(FactorsIdentifier.GetFormulaString).
                FindFirstNotNull();
        }
        private string GetBinaryFormulaString(IReadOnlyList<Digit> digits)
        {
            return digits.SplitIntoThreeParts().
                GetResults(digitsTuple => digitsTuple.ToLong()).
                GetResults(FactorsIdentifier.GetFormulaString).
                FindFirstNotNull();
        }
        private string GetOtherFormulaString(IReadOnlyList<Digit> digits, int arity)
        {
            return digits.SplitIntoParts(arity).
                    GetResults(parts => parts.ToLong()).
                    GetResults(FactorsIdentifier.GetFormulaString).
                    FindFirstNotNull();
        }

        public bool IsFormula(IReadOnlyList<Digit> digits)
        {
            return FactorsIdentifier.Arities.
                GetResults(arity => IsFormula(digits, arity)).
                Contains(true);
        }

        private bool IsFormula(IReadOnlyList<Digit> digits, int arity)
        {
            if (arity == 1) return IsUnaryFormula(digits);
            else if (arity == 2) return IsBinaryFormula(digits);
            else return IsOtherFormula(digits, arity);
        }
        private bool IsUnaryFormula(IReadOnlyList<Digit> digits)
        {
            return digits.SplitIntoTwoParts().
                GetResults(digitsTuple => digitsTuple.ToLong()).
                GetResults(FactorsIdentifier.IsFormula).
                Contains(true);
        }
        private bool IsBinaryFormula(IReadOnlyList<Digit> digits)
        {
            return digits.SplitIntoThreeParts().
                GetResults(digitsTuple => digitsTuple.ToLong()).
                GetResults(FactorsIdentifier.IsFormula).
                Contains(true);
        }
        private bool IsOtherFormula(IReadOnlyList<Digit> digits, int arity)
        {
            return digits.SplitIntoParts(arity).
                GetResults(parts => parts.ToLong()).
                GetResults(FactorsIdentifier.IsFormula).
                Contains(true);
        }

    }
}
