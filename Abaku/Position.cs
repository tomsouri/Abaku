using System;

namespace CommonTypes
{
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
        public (Position, Position, Position,Position) GetAdjacentPositions()
        {
            return (new Position(Row + 1, Column), new Position(Row - 1, Column), new Position(Row, Column + 1), new Position(Row, Column - 1));
        }
    }
}
