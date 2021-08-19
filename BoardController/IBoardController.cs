using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace BoardController
{
    public interface IBoardController
    {
        /// <summary>
        /// Checks, whether the given move is valid in the current situation or not.
        /// </summary>
        /// <param name="move">The move to check.</param>
        /// <returns>True if the move is valid.</returns>
        bool IsValid(Move move);

        /// <summary>
        /// If the move is valid, evaluates it and returns score you get for playing the move.
        /// If the move is not valid, returns score you get (that is usually some negative score).
        /// </summary>
        /// <param name="move">The move to evaluate.</param>
        /// <returns>Score you get by applying the given move.</returns>
        int Evaluate(Move move);

        /// <summary>
        /// Finds all formulas included in the move in current situation and computes their score.
        /// Is is used during the game for displaying all the formulas after applying a move,
        /// included the score you get from the formula.
        /// </summary>
        /// <param name="move">The applyed move.</param>
        /// <returns>List of FormulaRepresentations.</returns>
        IReadOnlyList<FormulaRepresentation> AllFormulasIncludedIn(Move move);

        /// <summary>
        /// Checks the validity of the move. If it is valid, computes the score and changes the insides of board.
        /// If the move is not valid, does not change the insides of the board.
        /// </summary>
        /// <param name="move">The move to enter.</param>
        /// <returns>The score got playing this move.</returns>
        int EnterMove(Move move);



        /// <summary>
        /// Method used by smart agent. Finds the best move in the current situation.
        /// </summary>
        /// <param name="availableStones"> Stones on hand (digits that can be used).</param>
        /// <returns>The best move in the current situation.</returns>
        EvaluatedMove? GetBestMove(IReadOnlyList<Digit> availableStones);

        /// <summary>
        /// Enters move WITHOUT checking its validity. Do not use in normal game!
        /// </summary>
        /// <param name="move"></param>
        void EnterMoveUnsafe(Move move);


        /// <summary>
        /// Is used for configuration of allowed mathematical operations. 
        /// The game environment asks for the tools and after choosing
        /// the settings, it calls the Setup() methods on all chosen setup tools.
        /// More of them can be used.
        /// </summary>
        /// <returns>List of tools representing possible mathematical operations to allow.</returns>
        IReadOnlyList<ISetupTool> GetOperationSetupTools();

        /// <summary>
        /// Is used for configuration of evaluation (which type of evaluation is used). 
        /// The game environment asks for the tools and after choosing
        /// the settings, it calls the Setup() methods on all chosen setup tools.
        /// Only one of them can be used (only the first called has the effect).
        /// </summary>
        /// <returns>List of tools representing possible evaluations to choose.</returns>
        IReadOnlyList<ISetupTool> GetEvaluationSetupTools();

        /// <summary>
        /// Is used for configuration of board (which type of evaluation board
        /// is used, the size of board and the location of the starting position). 
        /// The game environment asks for the tools and after choosing
        /// the settings, it calls the Setup() methods on all chosen setup tools.
        /// Only one of them can be used (only the first called has the effect).
        /// </summary>
        /// <returns>List of tools representing possible evaluation boards to choose.</returns>
        IReadOnlyList<ISetupTool> GetBoardSetupTools();

        /// <summary>
        /// Is used for configuration of evaluation of invalid moves. 
        /// The game environment asks for the tools and after choosing
        /// the settings, it calls the Setup() methods on all chosen setup tools.
        /// Only one of them can be used (only the first called has the effect).
        /// </summary>
        /// <returns>List of tools representing possible evaluations of invalid moves.</returns>
        IReadOnlyList<ISetupTool> GetInvalidMoveEvaluationSetupTools();


        /// <summary>
        /// If a digit is null, it means, that no digit is placed on the position.
        /// </summary>
        /// <returns>The copy of the board, 2D array of nullable digits.</returns>
        Digit?[][] GetBoardContent();
    }
}
