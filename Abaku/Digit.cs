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
    public struct Digit :IEquatable<Digit>
    {
        private byte Value { get; }
        private static readonly byte zero = 0;
        private static readonly byte distinctDigits = 10;
        public static int DistinctDigits => distinctDigits;
        public static Digit ZERO { get => (Digit)zero; }
        public Digit(byte val)
        {
            if (val >= distinctDigits)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(val), "The argument must be a valid decimal digit");
            }
            Value = val;
        }
        public static explicit operator Digit (int i) => new Digit((byte)i);
        public static implicit operator int (Digit d) => d.Value;
        public static implicit operator byte (Digit d) => d.Value;


        public static bool operator ==(Digit a, Digit b) => a.Value == b.Value;
        public static bool operator !=(Digit a, Digit b) => a.Value != b.Value;
        public bool Equals(Digit other)
        {
            return this == other;
        }
        public override bool Equals(object obj)
        {
            if (obj is Digit d) return Equals(d);
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
