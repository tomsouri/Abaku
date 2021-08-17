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
        private IFormulaIdentifier FormulaIdentifier => OpManager.FlaIdentifier;
        private IBoardManager BoardManager { get; }
        private IBoard Board => BoardManager.Board;
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
            return Validator.IsValid(move, Board, FormulaIdentifier);
        }
        int IBoardController.Evaluate(Move move)
        {
            return EvalManager.Evaluate(move, Board, FormulaIdentifier, Validator.IsValid);
        }
        IReadOnlyList<FormulaRepresentation> IBoardController.AllFormulasIncludedIn(Move move)
        {
            return EvalManager.GetAllFormulasIncludedIn(move, Board, FormulaIdentifier, Validator.IsValid).ToArray();
        }

        int IBoardController.EnterMove(Move move)
        {
            var score = ((IBoardController)this).Evaluate(move);
            if (((IBoardController)this).IsValid(move))
            {
                EnterMoveUnsafe(move);
            }
            return score;
        }
        private void EnterMoveUnsafe(Move move)
        {
            BoardManager.EnterMove(move);
        }

        EvaluatedMove? IBoardController.GetBestMoves(IReadOnlyList<Digit> availableStones)
        {
            return Optimizer.GetBestMove(availableStones, (IExtendedBoard)Board, FormulaIdentifier,
                (IUnsafeEvaluator)EvalManager, (IUnsafeValidator)Validator);
        }
        void IBoardController.EnterMoveUnsafe(Move move)
        {
            EnterMoveUnsafe(move);
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
            return EvalManager.GetBoardSetupTools(((IBoardSetupper)BoardManager).Setup);
        }

        public IReadOnlyList<ISetupTool> GetInvalidMoveEvaluationSetupTools()
        {
            throw new NotImplementedException();
        }

        Digit?[][] IBoardController.GetBoardContent()
        {
            return BoardManager.GetBoardContent();
        }
    }
}
