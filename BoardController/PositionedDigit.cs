using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace BoardController
{
    namespace EvaluationManager
    {
        internal struct PositionedDigit
        {
            public Digit digit { get; }
            public Position position { get; }
            public bool IsNewlyPlaced { get; }
        }
    }

}
