using CommonTypes;
using System;

namespace BoardManaging
{
    public class BoardManager : IBoardManager, IBoardSetupper
    {
        private static readonly int defaultColumnsCount = 15;
        private static readonly int defaultRowsCount = defaultColumnsCount;
        private static readonly Position defaultStartPosition = 
                                new Position((defaultRowsCount - 1) / 2, (defaultColumnsCount - 1) / 2);
        public IBoard Board { get; private set; }

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
