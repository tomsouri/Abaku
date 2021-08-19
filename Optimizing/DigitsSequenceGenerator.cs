using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace Optimizing
{
    internal static class DigitsSequenceGenerator
    {
        public static IEnumerable<Digit[]> GetAllSequences(IReadOnlyList<Digit> availableDigits)
        {
            var stack = new Stack<Digit>();
            var digits = new List<Digit>();
            foreach (var digit in availableDigits)
            {
                digits.Add(digit);
            }
            return GetSequencesFrom(stack, digits);
        }

        private static IEnumerable<Digit[]> GetSequencesFrom(Stack<Digit> temporaryResultStack, List<Digit> availableDigits)
        {
            foreach (var distinctDigit in availableDigits.GetDistinct())
            {
                temporaryResultStack.Push(distinctDigit);
                yield return temporaryResultStack.ToArray();

                availableDigits.Remove(distinctDigit);

                foreach (var sequence in GetSequencesFrom(temporaryResultStack, availableDigits))
                {
                    yield return sequence;
                }
                availableDigits.Add(distinctDigit);
                temporaryResultStack.Pop();
            }
        }
    }

    internal static class DigitsEnumerableExtensions
    {
        private static readonly bool[] digitsUsed = new bool[Digit.DistinctDigits];
        /// <summary>
        /// For an enumerable of digits returns only distinct items, that is, ignores repeated items.
        /// Is not thread safe.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable<Digit> GetDistinct(this IEnumerable<Digit> enumerable)
        {
            for (int i = 0; i < digitsUsed.Length; i++)
            {
                digitsUsed[i] = false;
            }

            foreach (Digit digit in enumerable)
            {
                if (!digitsUsed[digit])
                {
                    yield return digit;
                    digitsUsed[digit] = true;
                }
            }
        }
    }
}
