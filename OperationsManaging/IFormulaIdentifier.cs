using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace OperationsManaging
{
    public interface IFormulaIdentifier
    {
        /// <summary>
        /// Determines, whether an array of digits represents a formula or not,
        /// usually depending on the setting of FactorsFormulaIdentifier.
        /// </summary>
        /// <param name="digits">List of digits to identify as formula.</param>
        /// <returns>True if it is formula.</returns>
        bool IsFormula(IReadOnlyList<Digit> digits);

        /// <summary>
        /// Determines, whether an array of digits contains a formula or not.
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="index">Index of a digit which must be contained in the formula.</param>
        /// <returns></returns>
        bool ContainsFormulaIncludingIndex(IReadOnlyList<Digit> digits, int index);

        /// <summary>
        /// Determines, whether an array of digits contains a formula or not.
        /// Works also for index1=index2.
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="index1">Index of a digit which must be contained in the formula.</param>
        /// <param name="index2">Other index of a digit which must be contained in the formula.</param>
        /// <returns></returns>
        bool ContainsFormulaIncludingIndices(IReadOnlyList<Digit> digits, int index1, int index2);

        /// <summary>
        /// Depending on allowed operations, creates an instance of FormulaRepresentation to represent a formula.
        /// </summary>
        /// <param name="digits">The digits, that build the formula.</param>
        /// <returns></returns>
        string GetFormulaString(IReadOnlyList<Digit> digits);
    }
}
