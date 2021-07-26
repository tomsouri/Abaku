using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    /// <summary>
    /// Represents a valid digit in a decimal system, that is, can contain bytes 0, 1, ..., 9.
    /// </summary>
    public struct Digit
    {
        private byte value { get; }
        public Digit(byte val)
        {
            if (val >= 10)
            {
                throw new ArgumentOutOfRangeException("The argument " + nameof(val) + " must be a valid decimal digit");
            }
            value = val;
        }
        public static explicit operator Digit (int i) => new Digit((byte)i);
        public static implicit operator int (Digit d) => d.value;
        public static implicit operator byte (Digit d) => d.value;
    }
}
