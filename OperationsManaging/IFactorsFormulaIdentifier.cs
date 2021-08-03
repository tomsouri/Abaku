using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal interface IFactorsFormulaIdentifier
    {
        bool IsFormula(long aFactor, long bFactor);
        bool IsFormula(long aFactor, long bFactor, long cFactor);
        bool IsFormula(long[] factors);

        /// <summary>
        /// Generates string representation of given formula.
        /// If there are more possilities, generates
        /// one of them.
        /// If it is not a formula, returns null.
        /// </summary>
        /// <returns>String representaion of formula or null if the formula is not valid.</returns>
        string GetFormulaString(long aFactor, long bFactor);
        /// <summary>
        /// Generates string representation of given formula.
        /// If there are more possilities, generates
        /// one of them.
        /// If it is not a formula, returns null.
        /// </summary>
        /// <returns>String representaion of formula or null if the formula is not valid.</returns>
        string GetFormulaString(long aFactor, long bFactor, long cFactor);
        /// <summary>
        /// Generates string representation of given formula.
        /// If there are more possilities, generates
        /// one of them.
        /// If it is not a formula, returns null.
        /// </summary>
        /// <returns>String representaion of formula or null if the formula is not valid.</returns>
        string GetFormulaString(long[] factors);
    }
}
