using System;
using CommonTypes;

namespace BoardController
{
    public interface IBoardController
    {
        bool IsValid(Move move);
        int Evaluate(Move move);

    }
}
