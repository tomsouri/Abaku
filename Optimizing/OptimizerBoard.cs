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
        /// <summary>
        /// For the given count of digits to be placed returns all empty positions,
        /// that, in the given direction, contains enough empty positions beyond
        /// and not to much nonadjacent positions beyond, that is, all positions,
        /// where we can start to place the digits and end up with a positionally
        /// valid move.
        /// </summary>
        /// <param name="digitsCount"></param>
        /// <returns></returns>
        IEnumerable<Position> GetPositionsSuitableForDigitsCount(Direction direction, int digitsCount);

        /// <summary>
        /// Return the RO list including count empty positions, starting with the given empty position
        /// and continuing with other empty positions, that lie in the given direction beyond the given position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <returns>RO list of Positions to be placed into Move struct.</returns>
        IReadOnlyList<Position> GetEmptyPositionsBeyond(Position position, Direction direction, int count);

    }
    internal class OptimizerBoard : IOptimizerBoard
    {
        private Board _board;

        /// <summary>
        /// For every simple direction and every length of digit sequence it represents the list of positions
        /// that are suitable for that length - that is, there is enough empty cells beyond to use all digits
        /// from the sequence, and there is not so many nonadjacent cells directly following up the cell,
        /// so the digits won't be totally nonadjacent if placed here.
        /// </summary>
        private DirectionIndexedTuple<List<Position>[]> SuitablePositionsLists;
        public OptimizerBoard(IExtendedBoard extendedBoard, int digitsCount)
        {
            _board = CreateBoard(extendedBoard);
            InitializeSuitablePositionsLists(digitsCount);
        }
        private Board CreateBoard(IExtendedBoard extendedBoard)
        {
            var rowsCount = extendedBoard.RowsCount;
            var colsCount = extendedBoard.ColumnsCount;
            var board = new Board(rowsCount, colsCount);
            board.LoadEmptyCells(extendedBoard);
            board.LoadAdjacentCells(extendedBoard);
            board.LoadEmptyCellsCounts();
            board.LoadNonAdjacentCellsCounts();
            board.LoadEmptyCellsArrays();
            return board;
        }

        /// <summary>
        /// Initializes the lists of suitable positions, for every direction and for every possible
        /// length of placed digit sequence.
        /// </summary>
        /// <param name="digitsCount">Maximal length of the placed digit sequence.</param>
        private void InitializeSuitablePositionsLists(int digitsCount)
        {
            foreach (var direction in Direction.SimpleDirections)
            {
                var lists = new List<Position>[digitsCount];

                for (int index = 0; index < digitsCount; index++)
                {
                    var sequenceLength = index + 1;
                    var list = new List<Position>();

                    foreach (var position in _board.GetAllPositions())
                    {
                        var cell = _board[position];
                        if (cell.IsEmpty && // we can start with this position
                            sequenceLength <= cell.EmptyCellsBeyondCounts[direction] && // the digits fit in
                            sequenceLength > cell.NonAdjCellsBeyondCounts[direction]) // the placed digits won't be totally
                                                                                      // nonadjacent if placed here
                        {
                            list.Add(position);
                        }
                    }

                    lists[index] = list;
                }

                SuitablePositionsLists[direction] = lists;
            }
        }
        public IEnumerable<Position> GetPositionsSuitableForDigitsCount(Direction direction, int digitsCount)
        {
            return SuitablePositionsLists[direction][digitsCount - 1];
        }

        public IReadOnlyList<Position> GetEmptyPositionsBeyond(Position position, Direction direction, int count)
        {
            return _board[position].GetEmptyPositionsBeyond(direction, count);
        }

        private class Board
        {
            public int RowsCount => _board.Length;
            public int ColumnsCount => _board[0].Length;
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
            public IEnumerable<Position> GetAllPositions()
            {
                for (int rowNumber = 0; rowNumber < RowsCount; rowNumber++)
                {
                    var row = new Cell[ColumnsCount];
                    for (int columnNumber = 0; columnNumber < row.Length; columnNumber++)
                    {
                        yield return new Position(rowNumber, columnNumber);
                    }
                }
            }
            private Position MaximalContainedPosition => new Position(RowsCount - 1, ColumnsCount - 1);

            /// <summary>
            /// Returns all positions contained in the board that are beyond the given position,
            /// including the given position (if it is inside the board).
            /// </summary>
            /// <param name="start"></param>
            /// <param name="direction"></param>
            /// <returns></returns>
            private IEnumerable<Position> GetPositionsBeyond(Position start, Direction direction)
            {
                var currentPosition = start;
                while (currentPosition <= MaximalContainedPosition)
                {
                    yield return currentPosition;
                }
            }

            /// <summary>
            /// Set the bool property of being empty in every cell in this board, based on the given ExtendedBoard.
            /// </summary>
            /// <param name="extendedBoard"></param>
            public void LoadEmptyCells(IExtendedBoard extendedBoard)
            {
                foreach (var position in GetAllPositions())
                {
                    this[position].IsEmpty = extendedBoard.IsPositionEmpty(position);
                }
            }

            /// <summary>
            /// Set the bool property of being adjacent to some occupied cell in every cell in this board,
            /// based on the given ExtendedBoard.
            /// </summary>
            /// <param name="extendedBoard"></param>
            public void LoadAdjacentCells(IExtendedBoard extendedBoard)
            {
                foreach (var position in GetAllPositions())
                {
                    this[position].IsAdjacent = extendedBoard.IsAdjacentToOccupiedPosition(position);
                }
            }

            /// <summary>
            /// Set the counts of nonadjacent cells directly following up the concrete cell (including the concrete cell),
            /// for every cell in this board and for every direction.
            /// </summary>
            public void LoadNonAdjacentCellsCounts()
            {
                foreach (var direction in Direction.SimpleDirections)
                {
                    foreach (var position in GetAllPositions())
                    {
                        int count = 0;
                        foreach (var positionBeyond in GetPositionsBeyond(position, direction))
                        {
                            if (this[positionBeyond].IsAdjacent) break;
                            else count++;
                        }
                        this[position].NonAdjCellsBeyondCounts[direction] = count;
                    }
                }
            }

            /// <summary>
            /// Set the counts of empty cells following up the concrete cell (including the concrete cell),
            /// for every cell in this board and for every direction.
            /// </summary>
            public void LoadEmptyCellsCounts()
            {
                foreach (var direction in Direction.SimpleDirections)
                {
                    foreach (var position in GetAllPositions())
                    {
                        int count = 0;
                        foreach (var positionBeyond in GetPositionsBeyond(position, direction))
                        {
                            if (this[positionBeyond].IsEmpty) count++;
                        }
                        this[position].EmptyCellsBeyondCounts[direction] = count;
                    }
                }
            }


            /// <summary>
            /// For every line (that is, for every column and row) and the corresponding direction
            /// create an array of empty positions in that line. Add the corresponding segment
            /// of that array to every cell in that line (to represent positions of empty cells
            /// beyond that cell, including the cell).
            /// </summary>
            public void LoadEmptyCellsArrays()
            {
                Position startingPosition = new Position(0, 0);

                foreach (Direction lineShift in Direction.SimpleDirections)
                {
                    Direction cellShift = lineShift.Flipped;
                    for (Position lineStartPosition = startingPosition; lineStartPosition
                        <= MaximalContainedPosition; lineStartPosition += lineShift)
                    {
                        int emptyPositionsInLineCount = this[lineStartPosition].EmptyCellsBeyondCounts[cellShift];
                        var emptyPositions = new Position[emptyPositionsInLineCount];

                        int currentIndex = 0;

                        for (Position currentPosition = lineStartPosition; currentPosition
                            <= MaximalContainedPosition; currentPosition += cellShift)
                        {
                            this[currentPosition].EmptyPositionsBeyond[cellShift] = 
                                new ArraySegment<Position>(emptyPositions, currentIndex, 
                                    emptyPositions.Length - currentIndex);

                            if (this[currentPosition].IsEmpty)
                            {
                                emptyPositions[currentIndex] = currentPosition;
                                currentIndex++;
                            }
                        }

                    }
                }
            }
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

            /// <summary>
            /// For every simple direction: the count of empty cells that are in the given row/column
            /// beyond this cell (including this cell).
            /// </summary>
            public DirectionIndexedTuple<int> EmptyCellsBeyondCounts;

            /// <summary>
            /// For every simple direction: the count of nonadjacent cells that are in the given row/column
            /// directly beyond this cell (including this cell).
            /// </summary>
            public DirectionIndexedTuple<int> NonAdjCellsBeyondCounts;
            public DirectionIndexedTuple<IReadOnlyList<Position>> EmptyPositionsBeyond;
            public IReadOnlyList<Position> GetEmptyPositionsBeyond(Direction direction, int length)
            {
                return new ReadOnlyListSegment<Position>(EmptyPositionsBeyond[direction],0,length);
            }
        }

    }
}
