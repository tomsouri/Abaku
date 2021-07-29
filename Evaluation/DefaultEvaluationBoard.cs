using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation
{
    internal class DefaultEvaluationBoard : IEvaluationBoard
    {
        public PositionEvaluationInfo this[Position position] => throw new NotImplementedException();
    }
}
