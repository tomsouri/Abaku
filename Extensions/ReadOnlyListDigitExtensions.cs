using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace ReadOnlyListDigitExtensions
{
    public static class ReadOnlyListDigitExtensions
    {
        private static int BASE = 10;
        /// <summary>
        /// Converts list of digits to long representing the string of digits
        /// using Horner schema in decimal system.
        /// </summary>
        /// <param name="digits">List of digits to convert.</param>
        /// <returns>Long, the converted number.</returns>
        public static long ToLong(this IReadOnlyList<Digit> digits)
        {
            long result = 0;
            for (int i = 0; i < digits.Count; i++)
            {
                result *= BASE;
                result += digits[i];
            }
            return result;
        }
    }
}
