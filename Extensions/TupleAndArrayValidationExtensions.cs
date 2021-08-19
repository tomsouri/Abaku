using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace TupleAndArrayValidationExtensions
{
    public static class TupleAndArrayValidationExtensions
    {
        public static bool IsValid<T>(this (T,T) tuple, Predicate<T> predicate)
        {
            return predicate(tuple.Item1) && predicate(tuple.Item2);
        }
        public static bool IsValid<T>(this (T, T, T) tuple, Predicate<T> validate)
        {
            return validate(tuple.Item1) && validate(tuple.Item2) && validate(tuple.Item3);
        }
        public static bool IsValid<T>(this T[] array, Predicate<T> validate)
        {
            foreach (var item in array)
            {
                if (!validate(item)) return false;
            }
            return true;
        }
        public static IEnumerable<(T,T)> GetOnlyValidTuples<T>(this IEnumerable<(T,T)> source, Predicate<T> validate)
        {
            foreach (var tuple in source)
            {
                if (tuple.IsValid(validate))
                {
                    yield return tuple;
                }
            }
        }
        public static IEnumerable<(T,T,T)> GetOnlyValidTuples<T>(this IEnumerable<(T,T,T)> source, Predicate<T> validate)
        {
            foreach (var tuple in source)
            {
                if (tuple.IsValid(validate))
                {
                    yield return tuple;
                }
            }
        }
        public static IEnumerable<T[]> GetOnlyValidArrays<T>(this IEnumerable<T[]> source, Predicate<T> validate)
        {
            foreach (var array in source)
            {
                if (array.IsValid(validate))
                {
                    yield return array;
                }
            }
        }
    }
}
