using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace BoardManaging
{
    public interface IBoardManager
    {
        IBoard Board { get; }
        void EnterMove(Move move);
        Digit?[,] GetBoardContent();
    }
}
