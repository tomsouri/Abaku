using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace BoardController
{
    namespace EvaluationManager
    {
        struct Formula : IEnumerable<PositionedDigit>
        {
            private Position Start { get; }
            private Position End { get; }
            private IBoardAfterHypotheticalMove BoardAfterMove {get;}
            public IEnumerator<PositionedDigit> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }

}
