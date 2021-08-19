using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("InteractiveConsoleTests")]

namespace Optimizing
{
    internal static class DigitsSequenceGenerator
    {
        public static IEnumerable<Digit[]> GetAllSequences(IReadOnlyList<Digit> availableDigits)
        {
            var digits = new Digit[availableDigits.Count];
            for (int i = 0; i < availableDigits.Count; i++)
            {
                digits[i] = availableDigits[i];
            }
            Array.Sort(digits);
            return GetSequencesFrom(new Stack<Digit>(), digits);
        }
        private static IEnumerable<Digit[]> GetSequencesFrom(Stack<Digit> stack, IReadOnlyList<Digit> remainingDigits)
        {
            if (remainingDigits.Count == 0)
            {
                foreach (var seq in Enumerable.Empty<Digit[]>())
                {
                    yield return seq;
                } 
            }
            else
            {
                foreach (var (digit, remainder) in GetDigitAndRemainder(remainingDigits))
                {
                    stack.Push(digit);
                    yield return stack.ToArray();
                    //if (remainder.Count > 0)
                    //{
                    foreach (var sequence in GetSequencesFrom(stack, remainder))
                    {
                        yield return sequence;
                    }
                    //}
                    stack.Pop();
                }
            }
        }

        private static IEnumerable<(Digit digit, IReadOnlyList<Digit> remainder)> 
                                            GetDigitAndRemainder(IReadOnlyList<Digit> availableDigits)
        {
            if (availableDigits.Count == 0)
            {
                foreach (var item in Enumerable.Empty<(Digit,IReadOnlyList<Digit>)>())
                {
                    yield return item;
                }
            }
            else
            {
                var lastDigit = availableDigits[0];
                yield return (lastDigit, availableDigits.ExcludeItemOnIndex(0));

                for (int i = 1; i < availableDigits.Count; i++)
                {
                    var digit = availableDigits[i];
                    if (digit == lastDigit) { }
                    else
                    {
                        lastDigit = digit;
                        yield return (lastDigit, availableDigits.ExcludeItemOnIndex(i));
                    }
                }
            }
        }
    }
    internal static class Extensions
    {
        public static IReadOnlyList<T> ExcludeItemOnIndex<T>(this IReadOnlyList<T> collection, int excludedIndex)
        {
            var result = new T[collection.Count - 1];
            var resultIndex = 0;
            for (int inputIndex = 0; inputIndex < collection.Count; inputIndex++)
            {
                if (excludedIndex != inputIndex)
                {
                    result[resultIndex] = collection[inputIndex];
                    resultIndex++;
                }
            }
            return result;
        }
    }


    internal static class SequenceGenerator
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
