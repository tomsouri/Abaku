using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsManaging
{
    internal interface IUniversalFactorsFormulaIdentifier : IFactorsFormulaIdentifier
    {
        IEnumerable<int> Arities { get; }
        void Add(ISimpleFactorsFormulaIdentifier identifier);

        bool IsFormula((long a, long b) factors);
        bool IsFormula((long a, long b, long c) factors);


        /// <summary>
        /// Generates string representation of given formula.
        /// If there are more possilities, generates
        /// one of them.
        /// If it is not a formula, returns null.
        /// </summary>
        /// <returns>String representaion of formula or null if the formula is not valid.</returns>
        string GetFormulaString((long a, long b) factors);
        /// <summary>
        /// Generates string representation of given formula.
        /// If there are more possilities, generates
        /// one of them.
        /// If it is not a formula, returns null.
        /// </summary>
        /// <returns>String representaion of formula or null if the formula is not valid.</returns>
        string GetFormulaString((long a, long b, long c) factors);

    }
}
