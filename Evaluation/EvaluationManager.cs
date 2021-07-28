using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using OperationsManaging;
using BoardManaging;

namespace Evaluation
{
    internal class EvaluationManager : IEvaluationManager
    {
        private FormulaEvaluationDelegate CurrentFormulaEvaluation { get; set; }

        private IEnumerable<Formula> GetAllFormulas(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            throw new NotImplementedException();
        }

        public int Evaluate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            return Evaluate(move, board, formulaIdentifier, CurrentFormulaEvaluation);
        }
        public int Evaluate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, FormulaEvaluationDelegate formulaEvaluation)
        {
            int score = 0;
            foreach (var formula in GetAllFormulas(move, board, formulaIdentifier))
            {
                score += formulaEvaluation(formula);
            }
            return score;
        }

        public IEnumerable<FormulaRepresentation> GetAllFormulasIncludedIn(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            return GetAllFormulasIncludedIn(move, board, formulaIdentifier, CurrentFormulaEvaluation);
        }

        public IEnumerable<FormulaRepresentation> GetAllFormulasIncludedIn(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, FormulaEvaluationDelegate formulaEvaluation)
        {
            foreach (var formula in GetAllFormulas(move, board, formulaIdentifier))
            {

                var (start, end) = formula.GetBounds();
                var section = new BoardAfterMove(board,move).GetSection(start, end);
                var formulaRepresentation = formulaIdentifier.GetFormulaRepresentation(section);

                formulaRepresentation.Score = formulaEvaluation(formula);
                yield return formulaRepresentation;
            }
        }

        private struct BoardAfterMove
        {
            private Move move { get; }
            private IBoard board { get; }
            public BoardAfterMove(IBoard board, Move move)
            {
                this.move = move;
                this.board = board;
            }
            public IReadOnlyList<Digit> GetSection(Position start, Position end)
            {
                throw new NotImplementedException();
            }
        }

        private struct Formula : IEnumerable<PositionedDigit>
        {
            private Position Start { get; }
            private Position End { get; }
            private BoardAfterMove BoardAfterMove { get; }
            public IEnumerator<PositionedDigit> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            public (Position, Position) GetBounds()
            {
                return (Start, End);
            }
        }
    }
}
