using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using BoardManaging;

namespace Optimizing
{
    internal interface IOptimizerBoard
    {
        
    }
    internal class OptimizerBoard// : IOptimizerBoard
    {
        private Board _board;
        public OptimizerBoard(IExtendedBoard extendedBoard)
        {
            _board = Initialize(extendedBoard);
        }
        private Board Initialize(IExtendedBoard extendedBoard)
        {
            var rowsCount = extendedBoard.RowsCount;
            var colsCount = extendedBoard.ColumnsCount;
            var board = new Board(rowsCount, colsCount);
            LoadEmptyCells(extendedBoard,board);
            // TODO: dalsi inicializace
            return board;
        }
        private void LoadEmptyCells(IExtendedBoard extendedBoard, Board target)
        {
            var rowsCount = extendedBoard.RowsCount;
            var colsCount = extendedBoard.ColumnsCount;

            for (int rowIndex = 0; rowIndex < rowsCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < colsCount; colIndex++)
                {
                    var position = new Position(rowIndex, colIndex);
                    target[position].IsEmpty = extendedBoard.IsPositionEmpty(position);
                }
            }
        }
        private class Board
        {
            public Board(int rowsCount, int columnsCount)
            {
                _board = new Cell[rowsCount][];

                for (int i = 0; i < rowsCount; i++)
                {
                    var row = new Cell[columnsCount];
                    for (int j = 0; j < row.Length; j++)
                    {
                        row[j] = new Cell();
                    }
                    _board[i] = row;
                }
            }
            private Cell[][] _board;
            public Cell this[Position position] => _board[position.Row][position.Column];
        }
        private struct DirectionIndexedTuple<T>
        {
            private (T rowItem, T columnItem) Items;
            public T this[Direction direction]
            {
                get
                {
                    if (direction.RowDirection != 0) return Items.rowItem;
                    else return Items.columnItem;
                }
                set
                {
                    if (direction.RowDirection != 0) Items.rowItem = value;
                    else Items.columnItem = value;
                }
            }
        }
        private class Cell
        {
            public bool IsAdjacent { get; set; }
            public bool IsEmpty { get; set; }
            public DirectionIndexedTuple<byte> FreeCellsBeyondCounts { get; set; }
            public DirectionIndexedTuple<byte> NonAdjCellsBeyondCounts { get; set; }
            public DirectionIndexedTuple<IReadOnlyList<Position>> EmptyPositionsBeyond { get; set; }
            public IReadOnlyList<Position> GetEmptyPositionsBeyond(Direction direction, int length)
            {
                return new ReadOnlyListSegment<Position>(EmptyPositionsBeyond[direction],0,length);
            }
        }

    }
}
