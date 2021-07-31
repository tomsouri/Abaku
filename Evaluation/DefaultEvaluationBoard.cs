using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation
{
    internal class DefaultEvaluationBoard : IEvaluationBoard
    {
        // TODO: implementovat defaultEvalBoard.
        private DefaultEvaluationBoard() { }
        private static DefaultEvaluationBoard SingletonInstance { get; }
        static DefaultEvaluationBoard()
        {
            SingletonInstance = new DefaultEvaluationBoard();
        }
        public static DefaultEvaluationBoard Instance => SingletonInstance;
        public PositionEvaluationInfo this[Position position] => throw new NotImplementedException();

        public string Description => "Default evaluation board";

        public (int columns, int rows) GetSize()
        {
            throw new NotImplementedException();
        }

        public Position GetStartPosition()
        {
            throw new NotImplementedException();
        }
    }
}
