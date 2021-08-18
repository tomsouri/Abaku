using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnumerableCreatingExtensions;
using EnumerablePositionExtensions;

namespace BoardManaging
{
    internal class Board : IBoard, IExtendedBoard, IManagableBoard
    {
        private int RowsCount => _board.Length;
        private int ColumnsCount => _board[0].Length;
        private Position StartPosition { get; }
        private static Position MinimalContainedPosition => new Position(0, 0);
        private Position MaximalContainedPosition => new Position(RowsCount - 1, ColumnsCount - 1);
        private bool ContainsPosition(Position position) => MinimalContainedPosition <= position && position <= MaximalContainedPosition;
        private bool IsTotallyEmpty { get; set; }
        private Digit?[][] _board { get; set; }
        public Board(int rowsCount, int columnsCount, Position startPosition)
        {
            _board = new Digit?[rowsCount][];
            for (int i = 0; i < rowsCount; i++)
            {
                _board[i] = new Digit?[columnsCount];
            }
            StartPosition = startPosition;
            IsTotallyEmpty = true;
        }

        /// <summary>
        /// For every empty position and every non-existing position returns null.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Digit? this[Position position]
        {
            get
            {
                if (!ContainsPosition(position)) return null;
                return _board[position.Row][position.Column];
            }
            private set
            {
                IsTotallyEmpty = false;
                _board[position.Row][position.Column] = value;
            }
        }

        public bool ContainsZero(Position position)
        {
            var digit = this[position];
            return digit != null && digit == Digit.ZERO;
        }

        public IEnumerable<Position> GetAdjacentOccupiedPositions(Position basePosition)
        {
            foreach (var potentialAdjacentPosition in basePosition.GetAdjacentPositions())
            {
                if (this[potentialAdjacentPosition] != null) yield return potentialAdjacentPosition;
            }
        }


        public (Position start, Position end) GetLongestFilledSectionBounds((Position first, Position last) ToBeContained, Position ignoreVacancy)
        {
            return GetLongestFilledSectionBounds(ToBeContained, ignoreVacancy.GetSingleEnumerable());
        }

        public (Position start, Position end) GetLongestFilledSectionBounds(IEnumerable<Position> ignoreVacancy)
        {
            return GetLongestFilledSectionBounds(ignoreVacancy.FindMinAndMax(), ignoreVacancy);
        }

        public (Position start, Position end) GetLongestFilledSectionBounds((Position first, Position last) ToBeContained, IEnumerable<Position> ignoreVacancy)
        {
            // vacancy check
            var (first, last) = ToBeContained;
            if (first > last) (first, last) = (last, first);

            // TODO: co kdyz jsou first a last stejne?
            var direction = first.GetDirectionTo(last);

            for (Position position = first + direction; position < last; position += direction)
            {
                if (this[position] == null && !ignoreVacancy.Contains(position))
                {
                    throw new InvalidOperationException("There is no filled section containing the given positions, since there is a vacant position in between, whose vacancy is not ignored.");
                }
            }

            return GetLongestFilledSectionBoundsWithoutVacancyCheckBetweenContained(
                ToBeContained, ToBeContained.first.GetDirectionTo(ToBeContained.last), ignoreVacancy);
        }

        public (Position start, Position end) GetLongestFilledSectionBounds(Position ToBeContained, Direction direction)
        {
            return GetLongestFilledSectionBoundsWithoutVacancyCheckBetweenContained((ToBeContained, ToBeContained), direction, Enumerable.Empty<Position>());
        }
        private (Position start, Position end) GetLongestFilledSectionBoundsWithoutVacancyCheckBetweenContained
            ((Position first, Position last) ToBeContained, Direction direction,
            IEnumerable<Position> ignoreVacancy)
        {
            var (first, last) = ToBeContained;

            if (first > last) (first, last) = (last, first);

            while (this[first - direction] != null || ignoreVacancy.Contains(first-direction)) first -= direction;
            while (this[last + direction] != null || ignoreVacancy.Contains(last+direction)) last += direction;

            return (first, last);
        }

        public IReadOnlyList<Digit> GetSectionAfterApplyingMove(Position start, Position end, Move move)
        {
            int length = end - start + 1;
            return GetSectionAfterApplyingMove(start, end, move, new Digit[length]);
        }

        public IReadOnlyList<Digit> GetSectionAfterApplyingMove(Position start, Position end, Move move, Digit[] auxiliaryArray)
        {
            if (auxiliaryArray == null) return GetSectionAfterApplyingMove(start, end, move);

            // what if start and end are the same?
            if (start == end)
            {
                var digit = this[start] ?? move[start];
                if (digit == null)
                {
                    // invalid move
                    throw new InvalidOperationException("This was invalid move, it does not place any digit to a vacant position between other placed digits.");
                }
                auxiliaryArray[0] = (Digit)digit;
                return new ArraySegment<Digit>(auxiliaryArray, 0, 1);
            }

            var direction = start.GetDirectionTo(end);

            int index = 0;
            for (Position currentPosition = start; currentPosition <= end; currentPosition +=direction)
            {
                var digit = this[currentPosition] ?? move[currentPosition];
                if (digit == null)
                {
                    // invalid move
                    throw new InvalidOperationException("This was invalid move, it does not place any digit to a vacant position between other placed digits.");
                    //return null; 
                }
                    
                else
                {
                    auxiliaryArray[index] = (Digit)digit;
                }
                index++;
            }

            int length = index;

            return new ArraySegment<Digit>(auxiliaryArray, 0, length);
        }

        public bool IsAdjacentToOccupiedPosition(Position position)
        {
            foreach (var potentiallyAdjacent in position.GetAdjacentPositions())
            {
                if (this[potentiallyAdjacent] != null) return true;
            }
            return false;
        }

        public bool IsEmpty()
        {
            return IsTotallyEmpty;
        }

        public bool IsPositionEmpty(Position position)
        {
            return ContainsPosition(position) && this[position] == null;
        }

        public bool IsStartingPosition(Position position)
        {
            return position == StartPosition;
        }

        /// <summary>
        /// Creates a copy of its internal board. That is the array of rows of nullable digits.
        /// Null represents an empty cell.
        /// </summary>
        /// <returns></returns>
        Digit?[][] IManagableBoard.GetBoardContent()
        {
            var result = new Digit?[RowsCount][];
            for (int rowNumber = 0; rowNumber < RowsCount; rowNumber++)
            {
                var row = new Digit?[ColumnsCount];
                for (int columnNumber = 0; columnNumber < ColumnsCount; columnNumber++)
                {
                    row[columnNumber] = _board[rowNumber][columnNumber];
                }
                result[rowNumber] = row;
            }
            return result;
        }

        int IExtendedBoard.RowsCount => RowsCount;

        int IExtendedBoard.ColumnsCount => ColumnsCount;

        Digit IManagableBoard.this[Position position] { set => this[position]=value; }
    }
}
