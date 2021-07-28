using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace BoardController
{
    public interface IBoardController
    {
        // bool IsValid(Move move);
        // int Evaluate(Move move);
        // IReadOnlyList<Formula> GetAllFormulas(Move move);
        int EnterMove(Move move);
        IReadOnlyList<Move> GetBestMoves(IReadOnlyList<Digit> AvailableStones);
        //
        // TODO: operations, evaluation types, types of board.
        //
    }
}
