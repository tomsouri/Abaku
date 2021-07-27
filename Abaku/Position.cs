using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    /// <summary>
    /// Represents a position on a 2D "chess" board,
    /// which is used for the game Abaku.
    /// We suppose that the number of column or row does not
    /// exceed the capacity of one byte, because Abaku
    /// is usually played on 15x15 board.
    /// </summary>
    public struct Position : IEquatable<Position>
    {
        public byte Row { get; }
        public byte Column { get; }
        public Position(byte row, byte column)
        {
            Row = row;
            Column = column;
        }
        public Position(int row, int column)
        {
            Row = (byte)row;
            Column = (byte)column;
        }
        public static bool HaveSameRow(Position p1, Position p2) => p1.Row == p2.Row;
        public static bool HaveSameColumn(Position p1, Position p2) => p1.Column == p2.Column;
        public static bool operator ==(Position p1, Position p2) => p1.Equals(p2);

        public static bool operator !=(Position p1, Position p2) => !p1.Equals(p2);

        public static bool operator <=(Position p1, Position p2) => p1.Row <= p2.Row && p1.Column <= p2.Column;
        public static bool operator >=(Position p1, Position p2) => p1.Row >= p2.Row && p1.Column >= p2.Column;
        public static int operator -(Position p1, Position p2) => p2.Row - p1.Row + p2.Column - p1.Column;
        public bool Equals(Position other)
        {
            return this.Row == other.Row && this.Column==other.Column;
        }

        public override bool Equals(object obj)
        {
            if (obj is Position other) return this.Equals(other);
            return false;
        }
        public override int GetHashCode()
        {
            return this.Row.GetHashCode() + this.Column.GetHashCode();
        }

        /// <summary>
        /// Computes all possible adjacent positions in 2D grid.
        /// </summary>
        /// <returns>Returns enumerable of 4 positions, 
        /// each of which can be invalid in the context of a concrete 2D board.</returns>
        public IEnumerable<Position> GetAdjacentPositions()
        {
            yield return new Position(Row - 1, Column);
            yield return new Position(Row, Column - 1);
            yield return new Position(Row + 1, Column);
            yield return new Position(Row, Column + 1);
        }


        /// <summary>
        /// Return all positions between specified positions..
        /// </summary>
        /// <param name="start">Starting position, is not included in the returned positions.</param>
        /// <param name="end">Ending position, is not included in the returned positions. 
        /// Must have the same row or the same column.
        /// Must be greater than the starting position.</param>
        /// <returns>Positions directly between specified positions.</returns>
        public static IEnumerable<Position> GetPositionsBetween(Position start, Position end)
        {
            if (!(start <= end)) throw new InvalidOperationException("Starting position must be less than ending position.");
            if (HaveSameRow(start, end))
            {
                for (int colNumber = start.Column + 1; colNumber < end.Column; colNumber++)
                {
                    yield return new Position(start.Row, colNumber);
                }
            }
            else if (HaveSameColumn(start, end))
            {
                for (int rowNumber = start.Row + 1; rowNumber < end.Row; rowNumber++)
                {
                    yield return new Position(rowNumber, start.Column);
                }
            }
            else throw new InvalidOperationException("Given positions must be in the same row or in the same column.");
        }
    }
}
