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
    internal class Board : IBoard, IExtendedBoard
    {
        private int RowsCount => _board.Length;
        private int ColumnsCount => _board[0].Length;
        private Position StartPosition { get; }
        private Position MinimalContainedPosition => new Position(0, 0);
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

            var direction = first.GetDirectionTo(last);

            for (Position position = first + direction; position < last; position += direction)
            {
                if (this[position] == null && !ignoreVacancy.Contains(position))
                {
                    throw new InvalidOperationException();
                }
            }

            return GetLongestFilledSectionBoundsWithoutVacancyCheck(
                ToBeContained, ToBeContained.first.GetDirectionTo(ToBeContained.last));
        }

        public (Position start, Position end) GetLongestFilledSectionBounds(Position ToBeContained, Direction direction)
        {
            return GetLongestFilledSectionBoundsWithoutVacancyCheck((ToBeContained, ToBeContained), direction);
        }
        private (Position start, Position end) GetLongestFilledSectionBoundsWithoutVacancyCheck
            ((Position first, Position last) ToBeContained, Direction direction)
        {
            var (first, last) = ToBeContained;

            if (first > last) (first, last) = (last, first);

            while (this[first - direction] != null) first -= direction;
            while (this[last + direction] != null) last += direction;

            return (first, last);
        }

        public IReadOnlyList<Digit> GetSectionAfterApplyingMove(Position start, Position end, Move move)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Digit> GetSectionAfterApplyingMove(Position start, Position end, Move move, Digit[] auxiliaryArray)
        {
            throw new NotImplementedException();
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

        int IExtendedBoard.RowsCount => RowsCount;

        int IExtendedBoard.ColumnsCount => ColumnsCount;
    }
}
