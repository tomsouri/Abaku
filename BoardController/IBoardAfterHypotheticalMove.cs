using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace BoardController
{
    /// <summary>
    /// Simplifying object for accessing content of a board after applying a move.
    /// </summary>
    internal interface IBoardAfterHypotheticalMove
    {
        Digit? this[Position position] { get; }
        (Position, Position) GetLongestFilledSectionBounds(Position p1, Position p2);
        IReadOnlyList<Digit> GetSection(Position start, Position end);
        bool ContainsFormulaIncludingPositions(Position included1, Position included2, IFormulaIdentifier formulaIdentifier);

    }
}
