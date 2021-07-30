using CommonTypes;
using System;

namespace BoardManaging
{
    public class BoardManager : IBoardManager
    {
        public IBoard Board => throw new NotImplementedException();

        public void EnterMove(Move move)
        {
            throw new NotImplementedException();
        }

        public Digit?[,] GetBoardContent()
        {
            throw new NotImplementedException();
        }
    }
}
