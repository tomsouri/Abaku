using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace BoardController
{
    internal interface IBoardAfterHypotheticalMove
    {
        Digit? this[Position position] { get; }
        (Position, Position) GetLongestFilledSectionBounds(Position p1, Position p2);
        IReadOnlyList<Digit> GetSection(Position start, Position end);

    }
}
