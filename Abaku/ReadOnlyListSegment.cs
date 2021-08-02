using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public struct ReadOnlyListSegment<T> : IReadOnlyList<T>
    {
        public ReadOnlyListSegment(IReadOnlyList<T> list, int startIndex, int count)
        {
            throw new NotImplementedException();
        }
        public T this[int index] => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
