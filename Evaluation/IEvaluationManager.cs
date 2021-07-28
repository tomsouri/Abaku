using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using BoardManaging;
using OperationsManaging;

namespace Evaluation
{
    public interface IEvaluationManager
    {
        int Evaluate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier);
        int Evaluate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, FormulaEvaluationDelegate formulaEvaluation);
        IEnumerable<FormulaRepresentation> GetAllFormulasIncludedIn(Move move, IBoard board, IFormulaIdentifier formulaIdentifier);
        IEnumerable<FormulaRepresentation> GetAllFormulasIncludedIn(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, FormulaEvaluationDelegate formulaEvaluation);
    }
}
