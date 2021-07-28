using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using Validation;
using Evaluation;
using OperationsManaging;
using BoardManaging;
using Optimizing;

namespace BoardController
{
    internal class BoardController : IBoardController
    {
        public int EnterMove(Move move)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Move> GetBestMoves(IReadOnlyList<Digit> availableStones)
        {
            throw new NotImplementedException();
        }
    }
}
