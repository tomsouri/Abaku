using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public struct FormulaRepresentation
    {
        private string FormulaRepresentationString { get; }
        public int Score { get; set; }
        public override string ToString()
        {
            return FormulaRepresentationString;
        }
        public FormulaRepresentation(int score, string representation)
        {
            FormulaRepresentationString = representation;
            Score = score;
        }
    }
}
