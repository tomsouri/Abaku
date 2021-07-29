using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace Evaluation
{
    internal interface IEvaluationBoard
    {
        PositionEvaluationInfo this[Position position] { get; }
    }
}
