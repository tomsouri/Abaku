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
    public struct Position
    {
        byte Row { get; }
        byte Column { get; }
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
        /// Computes all possible adjacent positions in 2D grid.
        /// </summary>
        /// <returns>Returns a 4-tuple of positions, 
        /// each of which can be invalid in the context of a concrete 2D board.</returns>
        public (Position, Position, Position,Position) GetAdjacentPositions()
        {
            return (new Position(Row + 1, Column), new Position(Row - 1, Column), new Position(Row, Column + 1), new Position(Row, Column - 1));
        }
    }
}
