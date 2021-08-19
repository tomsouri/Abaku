using CommonTypes;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation
{
    internal class DefaultEvaluationBoard : IEvaluationBoard
    {
        private DefaultEvaluationBoard()
        {
            // TODO: change to BoardEncoding, when using the complicated basic eval board
            var reader = new StringReader(BoardEncoding);
            //var reader = new StringReader(SimpleBoardEncoding);
            (_board, StartPosition) = EvaluationBoardBuilder.GetBoardFromReader(reader);
            reader.Dispose();

            Rows = _board.Count;
            Columns = _board[0].Count;
        }
        private static class EvaluationBoardBuilder
        {
            /// <summary>
            /// Reads the board from the given reader.
            /// DOES NOT DO ANY CHECKS FOR ERRORS.
            /// If the input is invalid, the method fails.
            /// </summary>
            /// <param name="reader"></param>
            /// <returns></returns>
            internal static (IReadOnlyList<IReadOnlyList<PositionEvaluationInfo>> board, Position start) GetBoardFromReader(TextReader reader)
            {
                string line;
                var lines = new List<string>();
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                var board = new PositionEvaluationInfo[lines.Count][];
                Position start = new Position();
                for (int i = 0; i < lines.Count; i++)
                {
                    var currentLine = lines[i];
                    if (currentLine.Contains(StartingPositionEncoding))
                    {
                        start = new Position(i, currentLine.IndexOf(StartingPositionEncoding));
                    }
                    board[i] = LineToArray(currentLine);
                }
                return (board, start);
            }
            private static char StartingPositionEncoding => '4';
            private static Dictionary<char, PositionEvaluationInfo> encodingDictionary 
                = new Dictionary<char, PositionEvaluationInfo>()
        {
            // normal cell
            { '1', new PositionEvaluationInfo(positionFactor: 1, formulaFactor: 1) },
            // cell with 2-multiplying the digit on it
            { '2', new PositionEvaluationInfo(positionFactor: 2, formulaFactor: 1) },
            // cell with 3-multiplying the digit on it
            { '3', new PositionEvaluationInfo(positionFactor: 3, formulaFactor: 1) },
            // the starting cell, 2-multiplying the points for the whole formula
            { StartingPositionEncoding, new PositionEvaluationInfo(positionFactor: 1, formulaFactor: 2) },
            // standard cell with 2-multiplying the points for the whole formula
            { '6', new PositionEvaluationInfo(positionFactor: 1, formulaFactor: 2) },
            // cell with 3-multiplying the points for the whole formula
            { '9', new PositionEvaluationInfo(positionFactor: 1, formulaFactor: 3) }
        };

            /// <summary>
            /// If the string line contains some unknown characters,
            /// an error occurs.
            /// </summary>
            /// <param name="line"></param>
            /// <returns></returns>
            private static PositionEvaluationInfo[] LineToArray(string line)
            {
                var array = new PositionEvaluationInfo[line.Length];
                for (int i = 0; i < line.Length; i++)
                {
                    array[i] = encodingDictionary[line[i]];
                }
                return array;
            }
        }
        
        private static DefaultEvaluationBoard SingletonInstance { get; }
        static DefaultEvaluationBoard()
        {
            SingletonInstance = new DefaultEvaluationBoard();
        }
        public static DefaultEvaluationBoard Instance => SingletonInstance;
        private static string BoardEncoding = @"911311191112119
112111212111311
131116111111121
211161121161113
111611111116111
111113111311611
121111212111121
911211141112119
121111212111121
116113111311111
111611111116111
311161121161112
121111111611131
113111212111211
911211191113119";
        private static string SimpleBoardEncoding = @"111111111111111
111111111111111
111111111111111
111111111111111
111111111111111
111111111111111
111111111111111
111111111111111
111111111111111
111111111111111
111111111111111
111111111111111
111111111111111
111111111111111
111111111111111
";
        private IReadOnlyList<IReadOnlyList<PositionEvaluationInfo>> _board { get; } 
        private int Rows { get; } 
        private int Columns { get; } 
        private Position StartPosition { get; }
        public PositionEvaluationInfo this[Position position] => _board[position.Row][position.Column];

        public string Description => "Default evaluation board";

        public (int columns, int rows) GetSize()
        {
            return (Columns, Rows);
        }
        public Position GetStartPosition() => StartPosition;
    }
}
