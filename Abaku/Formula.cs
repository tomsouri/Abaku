using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public struct Formula
    {
        private string FormulaRepresentation { get; }
        public int Score { get; }
        public override string ToString()
        {
            return FormulaRepresentation;
        }
        public Formula(int score, string representation)
        {
            FormulaRepresentation = representation;
            Score = score;
        }
    }
}
