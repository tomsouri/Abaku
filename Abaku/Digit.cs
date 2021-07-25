using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public struct Digit
    {
        private byte value { get; }
        public Digit(byte val)
        {
            value = val;
        }
        public static explicit operator Digit (int i) => new Digit((byte)i);
        public static implicit operator int (Digit d) => d.value;
        public static implicit operator byte (Digit d) => d.value;
    }
}
