using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    /// <summary>
    /// Represents unit direction from one position to other, which is in the same row or the same column.
    /// </summary>
    public struct Direction
    {
        public byte RowDirection { get; }
        public byte ColumnDirection { get; }
        internal Direction(int rowDirection, int columnDirection)
        {
            RowDirection = (byte)(rowDirection == 0 ? 0 : 1);
            ColumnDirection = (byte)(columnDirection == 0 ? 0 : 1);
        }
        /// <summary>
        /// Represents flipped direction (for direction (1,0) you get direction (0,1)).
        /// </summary>
        public Direction Flipped => new (this.ColumnDirection, this.RowDirection);
        public static IEnumerable<Direction> SimpleDirections
        {
            get
            {
                yield return new Direction(1, 0);
                yield return new Direction(0, 1);
            }
        }
        public static Position operator +(Position p, Direction d) => new (p.Row + d.RowDirection, p.Column + d.ColumnDirection);
        public static Position operator -(Position p, Direction d) => new (p.Row - d.RowDirection, p.Column - d.ColumnDirection);
        public static Position operator *(int i, Direction d) => new (d.RowDirection * i, d.ColumnDirection * i);
        public override string ToString()
        {
            return string.Format("(row:{0}, column:{1})", this.RowDirection, this.ColumnDirection);
        }
    }


    /// <summary>
    /// Represents a position on a 2D "chess" board,
    /// which is used for the game Abaku.
    /// We suppose that the number of column or row does not
    /// exceed the capacity of one byte, because Abaku
    /// is usually played on 15x15 board.
    /// </summary>
    public struct Position : IEquatable<Position>, IComparable<Position>
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

        /// <summary>
        /// For positions in the same row or the same column returns the unit direction from first to the second.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Direction GetDirectionTo(Position other)
        {
            return new Direction(other.Row - this.Row, other.Column - this.Column);
        }
        public static bool HaveSameRow(Position p1, Position p2) => p1.Row == p2.Row;
        public static bool HaveSameColumn(Position p1, Position p2) => p1.Column == p2.Column;
        public static bool operator ==(Position p1, Position p2) => p1.Equals(p2);

        public static bool operator !=(Position p1, Position p2) => !p1.Equals(p2);

        public static bool operator <=(Position p1, Position p2) => p1.Row <= p2.Row && p1.Column <= p2.Column;
        public static bool operator >=(Position p1, Position p2) => p1.Row >= p2.Row && p1.Column >= p2.Column;

        public static bool operator <(Position p1, Position p2) => p1 <= p2 && p1 != p2;
        public static bool operator >(Position p1, Position p2) => p1 >= p2 && p1 != p2;

        /// <summary>
        /// The sum of differences of rows and columns of the positions.
        /// Practical usage is for positions with the same column or the same row.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>The manhattan distance of the two points.</returns>
        public static int operator -(Position p1, Position p2) => p2.Row - p1.Row + p2.Column - p1.Column;
        public static Position operator +(Position p1, Position p2) => new(p1.Row + p2.Row, p1.Column + p2.Column);
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


        /// TODO: the implementation can be changed with using Direction struct.
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
            if (!(start <= end)) throw new InvalidOperationException("Starting position must be less or equal than ending position.");
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

        /// <summary>
        /// Works properly only for Positions that are in the same row or the same column.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Position other)
        {
            return Row.CompareTo(other.Row) + Column.CompareTo(other.Column);
        }
        public override string ToString()
        {
            return string.Format("(row:{0}, column:{1})", this.Row, this.Column);
        }
    }
    
}
