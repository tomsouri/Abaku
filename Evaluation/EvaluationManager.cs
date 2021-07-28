using System;
using System.Collections.Generic;
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
                var section = board.GetBoardAfterHypotheticalMove(move).GetSection(start, end);
                var formulaRepresentation = formulaIdentifier.GetFormulaRepresentation(section);

                formulaRepresentation.Score = formulaEvaluation(formula);
                yield return formulaRepresentation;
            }
        }
    }
}
