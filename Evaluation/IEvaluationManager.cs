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
        /// <summary>
        /// Checks the validity of the move and evaluates it.
        /// Invalid moves -> negative score
        /// Valid moves -> positive score
        /// </summary>
        /// <param name="move">Move to evaluate.</param>
        /// <param name="board">Current board.</param>
        /// <param name="formulaIdentifier">Used Formula Identifier.</param>
        /// <param name="validation">Validation method.</param>
        /// <returns>Score got by applying the move.</returns>
        int Evaluate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, MoveValidationDelegate validationDelegate);
        
        /// <summary>
        /// Finds all formulas included in the applied move.
        /// </summary>
        /// <param name="move">Applied move.</param>
        /// <param name="board">Current board.</param>
        /// <param name="formulaIdentifier">Formula Identifier to use.</param>
        /// <returns>The representations of all found formulas.</returns>
        IEnumerable<FormulaRepresentation> GetAllFormulasIncludedIn(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, MoveValidationDelegate validationDelegate);


        IReadOnlyList<ISetupTool> GetEvaluationSetupTools();
        IReadOnlyList<ISetupTool> GetBoardSetupTools(SetBoardSettingDelegate setBoardSettingDelegate);
    }
}
