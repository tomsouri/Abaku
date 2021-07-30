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
    public class BoardController : IBoardController
    {
        private IValidator Validator { get; }
        private IEvaluationManager EvalManager { get; }
        private IOperationManager OpManager { get; }
        private IBoardManager BoardManager { get; }
        private IOptimizer Optimizer { get; }
        public BoardController()
        {
            Validator = Validation.Validator.Instance;
            EvalManager = new EvaluationManager();
            OpManager = new OperationManager();
            BoardManager = new BoardManaging.BoardManager();
            Optimizer = new Optimizing.Optimizer();
        }
        bool IBoardController.IsValid(Move move)
        {
            throw new NotImplementedException();
        }
        int IBoardController.Evaluate(Move move)
        {
            throw new NotImplementedException();
        }
        IReadOnlyList<FormulaRepresentation> IBoardController.AllFormulasIncludedIn(Move move)
        {
            throw new NotImplementedException();
        }

        int IBoardController.EnterMove(Move move)
        {
            throw new NotImplementedException();
        }

        IReadOnlyList<Move> IBoardController.GetBestMoves(IReadOnlyList<Digit> availableStones)
        {
            throw new NotImplementedException();
        }
        void IBoardController.EnterMoveUnsafe(Move move)
        {
            throw new NotImplementedException();
        }
        IReadOnlyList<ISetupTool> IBoardController.GetOperationSetupTools()
        {
            throw new NotImplementedException();
        }

        IReadOnlyList<ISetupTool> IBoardController.GetEvaluationSetupTools()
        {
            throw new NotImplementedException();
        }

        IReadOnlyList<ISetupTool> IBoardController.GetBoardSetupTools()
        {
            throw new NotImplementedException();
        }


        
        Digit?[,] IBoardController.GetBoardContent()
        {
            throw new NotImplementedException();
        }

    }
}
