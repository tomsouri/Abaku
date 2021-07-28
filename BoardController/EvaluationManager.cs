using CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardController
{
    namespace EvaluationManager
    {
        class EvaluationManager : IEvaluationManager
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
                foreach (var formula in GetAllFormulas(move,board,formulaIdentifier))
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
                throw new NotImplementedException();
            }
        }
    }
}

