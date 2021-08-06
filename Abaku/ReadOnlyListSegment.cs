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
        private readonly IReadOnlyList<T> _list;
        private readonly int _startIndex;
        private readonly int _count;
        public ReadOnlyListSegment(IReadOnlyList<T> list, int startIndex, int count)
        {
            if (startIndex + count > list.Count) throw new ArgumentOutOfRangeException(paramName:nameof(count));
            if (startIndex < 0) throw new ArgumentOutOfRangeException(paramName:nameof(startIndex));
            if (count < 0) throw new ArgumentOutOfRangeException(paramName:nameof(count));
            _list = list;
            _startIndex = startIndex;
            _count = count;
        }

        public T this[int index] => _list[index + _startIndex];

        public int Count => _count;

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _list[_startIndex + i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
