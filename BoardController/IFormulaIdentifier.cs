using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace BoardController
{
    namespace OperationManager
    {
        /// <summary>
        /// Determines, whether an array of digits is (or contains) a formula or not.
        /// </summary>
        interface IFormulaIdentifier
        {
            bool IsFormula(IReadOnlyList<Digit> digits);

            /// <summary>
            /// Determines, whether an array of digits contains a formula or not.
            /// </summary>
            /// <param name="digits"></param>
            /// <param name="index">Index of a digit which must be contained in the formula.</param>
            /// <returns></returns>
            bool ContainsFormulaIncludingIndices(IReadOnlyList<Digit> digits, int index);

            /// <summary>
            /// Determines, whether an array of digits contains a formula or not.
            /// </summary>
            /// <param name="digits"></param>
            /// <param name="index1">Index of a digit which must be contained in the formula.</param>
            /// <param name="index2">Index of a digit which must be contained in the formula.</param>
            /// <returns></returns>
            bool ContainsFormulaIncludingIndices(IReadOnlyList<Digit> digits, int index1, int index2);
        }
    }

}
