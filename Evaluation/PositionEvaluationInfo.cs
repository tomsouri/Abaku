using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation
{
    internal struct PositionEvaluationInfo
    {
        /// <summary>
        /// Factor, which multiplies the point score of the digit that lies on that position.
        /// </summary>
        public int PositionFactor { get; }

        /// <summary>
        /// Factor, which multiplies the point score of the whole formula.
        /// </summary>
        public int FormulaFactor { get; }
        public PositionEvaluationInfo(int positionFactor, int formulaFactor)
        {
            PositionFactor = positionFactor;
            FormulaFactor = formulaFactor;
        }
    }
}
