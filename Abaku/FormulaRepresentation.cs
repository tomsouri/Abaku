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
        public FormulaRepresentation(string representation)
        {
            FormulaRepresentationString = representation;
            
            // is not yet evaluated
            Score = 0;
        }
    }
}
