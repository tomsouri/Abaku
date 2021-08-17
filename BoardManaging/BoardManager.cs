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
        private bool HasDefaultSetup { get; set; }
        public BoardManager()
        {
            Board = new Board(defaultRowsCount, defaultColumnsCount, defaultStartPosition);
            HasDefaultSetup = true;
        }

        public IBoard Board { get; private set; }

        public void EnterMove(Move move)
        {
            foreach (var (digit,position) in move)
            {
                ((IManagableBoard)Board)[position] = digit;
            }
        }

        public Digit?[][] GetBoardContent()
        {
            return ((IManagableBoard)Board).GetBoardContent();
        }


        void IBoardSetupper.Setup(int columns, int rows, Position startPosition)
        {
            // valid call only once
            if (HasDefaultSetup)
            {
                HasDefaultSetup = false;
                Board = new Board(rowsCount: rows,columnsCount: columns, startPosition);
            }
        }
    }
}
