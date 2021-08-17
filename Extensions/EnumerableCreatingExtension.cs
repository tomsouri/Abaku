using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumerableCreatingExtensions
{
    public static class EnumerableCreatingExtension
    {
        /// <summary>
        /// Creates an enumerable containing only one item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="singleItem"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetSingleEnumerable<T>(this T singleItem)
        {
            yield return singleItem;
        }
    }
}
