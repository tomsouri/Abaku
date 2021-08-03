using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReadOnlyListExtensions.Splitting;
using ReadOnlyListExtensions.Sections;
using ReadOnlyListDigitExtensions;
using EnumerableExtensions;

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

        /// <summary>
        /// Determines, whether the given list of digits
        /// contains a formula including the given index.
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="index">Index of the digit to be contained in the formula.</param>
        /// <returns>True if the list contains such a formula.</returns>
        public bool ContainsFormulaIncludingIndex(IReadOnlyList<Digit> digits, int index)
        {
            return digits.GetSectionsContainingIndex(index).AtLeastOneSatisfies(IsFormula);
        }

        /// <summary>
        /// Determines, whether the given list of digits
        /// contains a formula including the given indices.
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="index1">Index of the first digit to be contained in the formula.</param>
        /// <param name="index2">Index of the second digit to be contained in the formula.</param>
        /// <returns>True if the list contains such a formula.</returns>
        public bool ContainsFormulaIncludingIndices(IReadOnlyList<Digit> digits, int index1, int index2)
        {
            return digits.GetSectionsContainingIndices(index1, index2).AtLeastOneSatisfies(IsFormula);
        }

        /// <summary>
        /// Finds a string representation of a formula
        /// represented by the given list of digits.
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="arity"></param>
        /// <returns>String representation of a valid formula or null.</returns>
        public string GetFormulaString(IReadOnlyList<Digit> digits)
        {
            return FactorsIdentifier.Arities.
                GetResults(arity => GetFormulaString(digits, arity)).
                FindFirstNotNull();
        }

        /// <summary>
        /// Finds a string representation of a formula
        /// represented by the given list of digits
        /// with the given arity.
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="arity"></param>
        /// <returns>String representation of a valid formula or null.</returns>
        private string GetFormulaString(IReadOnlyList<Digit> digits, int arity)
        {
            if (arity == 1) return GetUnaryFormulaString(digits);
            else if (arity == 2) return GetBinaryFormulaString(digits);
            else return GetOtherFormulaString(digits, arity);
        }

        /// <summary>
        /// Finds a string representation of a unary formula
        /// represented by the given list of digits.
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="arity"></param>
        /// <returns>String representation of a valid formula or null.</returns>
        private string GetUnaryFormulaString(IReadOnlyList<Digit> digits)
        {
            
            return digits.SplitIntoTwoParts().
                GetResults(digitsTuple => digitsTuple.ToLong()).
                GetResults(FactorsIdentifier.GetFormulaString).
                FindFirstNotNull();
        }

        /// <summary>
        /// Finds a string representation of a binary formula
        /// represented by the given list of digits.
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="arity"></param>
        /// <returns>String representation of a valid formula or null.</returns>
        private string GetBinaryFormulaString(IReadOnlyList<Digit> digits)
        {
            return digits.SplitIntoThreeParts().
                GetResults(digitsTuple => digitsTuple.ToLong()).
                GetResults(FactorsIdentifier.GetFormulaString).
                FindFirstNotNull();
        }

        /// <summary>
        /// Finds a string representation of formula
        /// represented by the given list of digits,
        /// with the given arity, which is not 1 nor 2.
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="arity"></param>
        /// <returns>String representation of a valid formula or null.</returns>
        private string GetOtherFormulaString(IReadOnlyList<Digit> digits, int arity)
        {
            return digits.SplitIntoParts(arity).
                    GetResults(parts => parts.ToLong()).
                    GetResults(FactorsIdentifier.GetFormulaString).
                    FindFirstNotNull();
        }

        /// <summary>
        /// Determines, whether the given list of digits represents a formula.
        /// </summary>
        /// <param name="digits"></param>
        /// <returns>True if the list represents a formula.</returns>
        public bool IsFormula(IReadOnlyList<Digit> digits)
        {
            return FactorsIdentifier.Arities.
                GetResults(arity => IsFormula(digits, arity)).
                Contains(true);
        }

        /// <summary>
        /// Determines, whether the given list of digits represents
        /// a formula with the given arity.
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="arity"></param>
        /// <returns></returns>
        private bool IsFormula(IReadOnlyList<Digit> digits, int arity)
        {
            if (arity == 1) return IsUnaryFormula(digits);
            else if (arity == 2) return IsBinaryFormula(digits);
            else return IsOtherFormula(digits, arity);
        }

        /// <summary>
        /// Determines, whether the given list of digits represents a unary formula.
        /// </summary>
        /// <param name="digits">The list of digits.</param>
        /// <returns>true if the list represents a unary formula.</returns>
        private bool IsUnaryFormula(IReadOnlyList<Digit> digits)
        {
            return digits.SplitIntoTwoParts().
                GetResults(digitsTuple => digitsTuple.ToLong()).
                GetResults(FactorsIdentifier.IsFormula).
                Contains(true);
        }

        /// <summary>
        /// Determines, whether the given list of digits represents a binary formula.
        /// </summary>
        /// <param name="digits">The list of digits.</param>
        /// <returns>true if the list represents a binary formula.</returns>
        private bool IsBinaryFormula(IReadOnlyList<Digit> digits)
        {
            return digits.SplitIntoThreeParts().
                GetResults(digitsTuple => digitsTuple.ToLong()).
                GetResults(FactorsIdentifier.IsFormula).
                Contains(true);
        }

        /// <summary>
        /// Determines, whether the given list of digits represents other formula
        /// than binary or unary.
        /// </summary>
        /// <param name="digits">The given list of digits.</param>
        /// <param name="arity">Given arity.</param>
        /// <returns>True if the list represents such a formula.</returns>
        private bool IsOtherFormula(IReadOnlyList<Digit> digits, int arity)
        {
            return digits.SplitIntoParts(arity).
                GetResults(parts => parts.ToLong()).
                GetResults(FactorsIdentifier.IsFormula).
                Contains(true);
        }

    }
}
