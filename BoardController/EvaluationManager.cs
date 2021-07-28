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

            //private IEnumerable<>

            public int Evaluate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
            {
                throw new NotImplementedException();
            }

            public int Evaluate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, FormulaEvaluationDelegate formulaEvaluation)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<FormulaRepresentation> GetAllFormulasIncludedIn(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<FormulaRepresentation> GetAllFormulasIncludedIn(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, FormulaEvaluationDelegate formulaEvaluation)
            {
                throw new NotImplementedException();
            }
        }
    }

}
