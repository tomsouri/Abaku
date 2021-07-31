using CommonTypes;
using System;

namespace BoardManaging
{
    public class BoardManager : IBoardManager, IBoardSetupper
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

        void IBoardSetupper.Setup(int columns, int rows, Position startPosition)
        {
            // TODO: dovolit validni zavolani jenom jednou.
            throw new NotImplementedException();
        }
    }
}
