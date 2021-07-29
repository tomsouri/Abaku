using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace Evaluation
{
    internal struct PositionedDigit
    {
        public Digit Digit { get; }
        public Position Position { get; }
        public bool IsNewlyPlaced { get; }
        public PositionedDigit(Digit digit, Position position, bool isNewlyPlaced)
        {
            Digit = digit;
            Position = position;
            IsNewlyPlaced = isNewlyPlaced;
        }
    }
}
