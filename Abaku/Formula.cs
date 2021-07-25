using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    struct Formula
    {
        private string formulaRepresentation { get; }
        public int Score { get; }
        public override string ToString()
        {
            return formulaRepresentation;
        }
        public Formula(int score, string representation)
        {
            formulaRepresentation = representation;
            Score = score;
        }
    }
}
