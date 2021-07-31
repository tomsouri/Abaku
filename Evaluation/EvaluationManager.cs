﻿using System;
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
    public class EvaluationManager : IEvaluationManager
    {
        public EvaluationManager()
        {
            CurrentFormulaEvaluationDelegate = FormulaEvaluationManager.DefaultFormulaEvaluation;
            CurrentEvaluationBoard = EvaluationBoardManager.DefaultEvaluationBoardInstance;
            CurrentInvalidMoveEvalDelegate = InvalidMoveEvaluationManager.DefaultInvalidMoveEvaluation;
        }

        #region FormulaEvaluation

        /// <summary>
        /// The delegate used for evaluation of formulas.
        /// </summary>
        private FormulaEvaluationDelegate CurrentFormulaEvaluationDelegate { get; set; }

        /// <summary>
        /// Is used for evaluating formulas, that is, for a given formula returns a score.
        /// </summary>
        /// <param name="formula">The formula for evaluation.</param>
        /// <returns>Score got from the specified formula.</returns>
        private delegate int FormulaEvaluationDelegate(IEnumerable<PositionedDigit> formula,
                                                       IEvaluationBoard evaluationBoard);

        private delegate void SetFormulaEvaluationDelegate(FormulaEvaluationDelegate formulaEvaluationDelegate);

        private static class FormulaEvaluationManager
        {
            /// <summary>
            /// Standard formula evaluation. For every digit you get its value.
            /// If the digit is newly placed, its value is multiplyed by a factor,
            /// that is on the corresponding position.
            /// Also, if the digit is newly placed, the whole score for the formula
            /// is multiplyed by the formula-factor, that is on the corresponding position.
            /// </summary>
            /// <param name="formula">Formula to evaluate, IEnumerable<PositionedDigit>.</param>
            /// <param name="evaluationBoard">The description of multiplying factors.</param>
            /// <returns>The score you get from the given formula.</returns>
            public static int DefaultFormulaEvaluation(IEnumerable<PositionedDigit> formula, IEvaluationBoard evaluationBoard)
            {
                int score = 0;
                int formulaMultiplyingFactor = 1;
                foreach (var positionedDigit in formula)
                {
                    var pos = positionedDigit.Position;
                    var digit = positionedDigit.Digit;
                    if (positionedDigit.IsNewlyPlaced)
                    {
                        score += digit * evaluationBoard[pos].PositionFactor;
                        formulaMultiplyingFactor *= evaluationBoard[pos].FormulaFactor;
                    }
                    else
                    {
                        score += digit;
                    }
                }
                return score * formulaMultiplyingFactor;
            }
            public static IEnumerable<(string, FormulaEvaluationDelegate)> GetDescriptionsAndFormulaEvalDelegates()
            {
                yield return ("Default evaluation", DefaultFormulaEvaluation);
            }

        }
        private void SetFormulaEvaluation(FormulaEvaluationDelegate value)
        {
            // TODO: dovolit validni zavolani jenom jednou
            CurrentFormulaEvaluationDelegate = value;
        }
        private class FormulaEvaluationSetupTool : ISetupTool
        {
            private SetFormulaEvaluationDelegate SetDelegate { get; }
            private FormulaEvaluationDelegate FormulaEvalDelegate { get; }
            public FormulaEvaluationSetupTool(string description, FormulaEvaluationDelegate formulaEvalDelegate, SetFormulaEvaluationDelegate setDelegate)
            {
                Description = description;
                SetDelegate = setDelegate;
                FormulaEvalDelegate = formulaEvalDelegate;
            }
            public string Description { get; }

            public void Setup()
            {
                SetDelegate(FormulaEvalDelegate);
            }
        }
        public IReadOnlyList<ISetupTool> GetEvaluationSetupTools()
        {
            var list = new List<ISetupTool>();
            foreach (var (description, evaluation) in FormulaEvaluationManager.GetDescriptionsAndFormulaEvalDelegates())
            {
                list.Add(new FormulaEvaluationSetupTool(description, evaluation, this.SetFormulaEvaluation));
            }
            return list;
        }
        #endregion

        #region EvaluationBoard
        private IEvaluationBoard CurrentEvaluationBoard { get; set; }

        private delegate void SetEvaluationBoardDelegate(IEvaluationBoard evaluationBoard);
        private void SetEvaluationBoard(IEvaluationBoard value)
        {
            // TODO: dovolit validni zavolani jen jednou
            CurrentEvaluationBoard = value;
        }
        private static class EvaluationBoardManager
        {
            public static IEvaluationBoard DefaultEvaluationBoardInstance { get; }
            static EvaluationBoardManager()
            {
                DefaultEvaluationBoardInstance = DefaultEvaluationBoard.Instance;
            }
            
            public static IEnumerable<IEvaluationBoard> GetEvaluationBoards()
            {
                yield return DefaultEvaluationBoardInstance;
            }
        }
        private class EvaluationBoardSetupTool : ISetupTool
        {
            public string Description => EvaluationBoard.Description;

            private IEvaluationBoard EvaluationBoard { get; }
            private SetEvaluationBoardDelegate SetEvalBoardDelegate { get; }
            private SetBoardSettingDelegate SetBoardSettingDelegate { get; }
            public EvaluationBoardSetupTool(IEvaluationBoard evaluationBoard,
                                            SetEvaluationBoardDelegate setEvalBoardDelegate,
                                            SetBoardSettingDelegate setBoardSettingDelegate)
            {
                EvaluationBoard = evaluationBoard;
                SetEvalBoardDelegate = setEvalBoardDelegate;
                SetBoardSettingDelegate = setBoardSettingDelegate;
            }
            public void Setup()
            {
                SetEvalBoardDelegate(EvaluationBoard);
                var (columns,rows) = EvaluationBoard.GetSize();
                SetBoardSettingDelegate(columns, rows, EvaluationBoard.GetStartPosition());
            }
        }
        public IReadOnlyList<ISetupTool> GetBoardSetupTools(SetBoardSettingDelegate setBoardSettingDelegate)
        {
            var list = new List<ISetupTool>();
            foreach (var board in EvaluationBoardManager.GetEvaluationBoards())
            {
                list.Add(new EvaluationBoardSetupTool(board, SetEvaluationBoard, setBoardSettingDelegate));
            }
            return list;
        }
        #endregion

        #region InvalidMoveEvaluation
        /// <summary>
        /// The delegate used for evaluation of invalid moves.
        /// </summary>
        private InvalidMoveEvaluationDelegate CurrentInvalidMoveEvalDelegate { get; set; }

        private delegate int InvalidMoveEvaluationDelegate(Move move);
        private delegate void SetInvalidMoveEvaluationDelegate(InvalidMoveEvaluationDelegate invalidMoveEvaluationDelegate);

        private static class InvalidMoveEvaluationManager
        {
            public static int DefaultInvalidMoveEvaluation(Move move)
            {
                int score = 0;
                foreach (var (digit,_) in move)
                {
                    score += digit;
                }
                return score;
            }
            public static IEnumerable<(string, InvalidMoveEvaluationDelegate)> GetDescriptionsAndInvMoveDelegates()
            {
                yield return ("Default invalid move evaluator", DefaultInvalidMoveEvaluation);
            }
        }
        private void SetInvalidMoveEvaluation(InvalidMoveEvaluationDelegate value)
        {
            // TODO: dovolit validni zavolani jen jednou
            CurrentInvalidMoveEvalDelegate = value;
        }
        private class InvalidMoveEvaluationSetupTool : ISetupTool
        {
            public string Description { get; }
            private InvalidMoveEvaluationDelegate InvalMoveEvalDelegate { get; }
            private SetInvalidMoveEvaluationDelegate SetInvalMoveEvalDelegate { get;}
            public InvalidMoveEvaluationSetupTool(string description,
                                                  InvalidMoveEvaluationDelegate invalMoveEvalDelegate,
                                                  SetInvalidMoveEvaluationDelegate setInvalMoveEvalDelegate)
            {
                Description = description;
                InvalMoveEvalDelegate = invalMoveEvalDelegate;
                SetInvalMoveEvalDelegate = setInvalMoveEvalDelegate;
            }
            public void Setup()
            {
                SetInvalMoveEvalDelegate(InvalMoveEvalDelegate);
            }
        }
        public IReadOnlyList<ISetupTool> GetInvalidMoveEvaluationSetupTools()
        {
            var list = new List<ISetupTool>();
            foreach ( var (description, evaluation) in InvalidMoveEvaluationManager.GetDescriptionsAndInvMoveDelegates())
            {
                list.Add(new InvalidMoveEvaluationSetupTool(description, evaluation, this.SetInvalidMoveEvaluation));
            }
            return list;
        }
        #endregion

        /// <summary>
        /// Finds all formulae included in the move.
        /// </summary>
        /// <param name="move">Current move.</param>
        /// <param name="board">Current board.</param>
        /// <param name="formulaIdentifier">Current IFormulaIdentifier.</param>
        /// <returns>IEnumerable of Formulae (the nested structure).</returns>
        private IEnumerable<Formula> GetAllFormulas(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            throw new NotImplementedException();
        }


        int IEvaluationManager.Evaluate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, MoveValidationDelegate validationDelegate)
        {
            if (validationDelegate(move, board, formulaIdentifier))
            {
                return EvaluateValidMove(move, board, formulaIdentifier);
            }
            return EvaluateInvalidMove(move, board, formulaIdentifier);
        }

        /// <summary>
        /// Evaluates the move in the current situation using the default formula evaluation delegate.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>The score you get after applying the move.</returns>
        private int EvaluateValidMove(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            return EvaluateValidMove(move, board, formulaIdentifier, CurrentFormulaEvaluationDelegate);
        }
        private int EvaluateInvalidMove(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Evaluates a valid move in the current situation.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <param name="formulaEvaluation">The specified delegate used to evaluate each formula.</param>
        /// <returns></returns>
        private int EvaluateValidMove(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, FormulaEvaluationDelegate formulaEvaluation)
        {
            int score = 0;
            foreach (var formula in GetAllFormulas(move, board, formulaIdentifier))
            {
                score += formulaEvaluation(formula, this.CurrentEvaluationBoard);
            }
            return score;
        }

        /// <summary>
        /// Checks validity and uses the defalut formula evaluation delegate to find representations of all formulae and evaluate them.
        /// The expected usage is for displaying all formulae contained in the move with their scores
        /// after applying the move by a player.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>IEnumerable of FormulaRepresentation. If the move is not valid, returns empty enumerable.</returns>
        IEnumerable<FormulaRepresentation> IEvaluationManager.GetAllFormulasIncludedIn(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, MoveValidationDelegate validationDelegate)
        {
            if (!validationDelegate(move, board, formulaIdentifier)) return Enumerable.Empty<FormulaRepresentation>();
            else
            {
                return GetAllFormulasIncludedIn(move, board, formulaIdentifier, CurrentFormulaEvaluationDelegate);
            }
        }

        /// <summary>
        /// Uses the specified formula evaluation delegate to find representations of all formulae and evaluate them.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <param name="formulaEvaluation">The specified formula evaluation delegate.</param>
        /// <returns>IEnumerable of FormulaRepresentation.</returns>
        private IEnumerable<FormulaRepresentation> GetAllFormulasIncludedIn(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, FormulaEvaluationDelegate formulaEvaluation)
        {
            foreach (var formula in GetAllFormulas(move, board, formulaIdentifier))
            {

                var (start, end) = formula.GetBounds();
                var section = new BoardAfterMove(board,move).GetSection(start, end);
                var formulaRepresentation = formulaIdentifier.GetFormulaRepresentation(section);

                formulaRepresentation.Score = formulaEvaluation(formula, this.CurrentEvaluationBoard);
                yield return formulaRepresentation;
            }
        }

        /// <summary>
        /// Simplifying object for accessing content of a board after applying a move.
        /// </summary>
        private struct BoardAfterMove
        {
            private Move Move { get; }
            private IBoard Board { get; }

            public Digit? this[Position position] { 
                get {
                    foreach (var (digit,pos) in Move)
                    {
                        if (pos == position) return digit;
                    }
                    return Board[position];
                } 
            }
            public BoardAfterMove(IBoard board, Move move)
            {
                this.Move = move;
                this.Board = board;
            }
            public bool IsPlacedInTheMove(Position position)
            {
                return Move.GetPositions().Contains(position);
            }
            public IReadOnlyList<Digit> GetSection(Position start, Position end)
            {
                return Board.GetSectionAfterApplyingMove(start, end, Move);
            }
        }

        private struct Formula : IEnumerable<PositionedDigit>
        {
            private Position Start { get; }
            private Position End { get; }
            private BoardAfterMove BoardAfterMove { get; }
            public IEnumerator<PositionedDigit> GetEnumerator()
            {
                var unitDelta = new PositionDelta(End, Start).GetUnitDelta();
                var currentPosition = Start;
                for (int i = 0; i < End-Start; i++)
                {
                    // we expect that the digit is not null!
                    var digit = (Digit)BoardAfterMove[currentPosition];
                    bool isNewlyPlaced = BoardAfterMove.IsPlacedInTheMove(currentPosition);

                    yield return new PositionedDigit(digit, currentPosition, isNewlyPlaced);

                    currentPosition += unitDelta;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            public (Position, Position) GetBounds()
            {
                return (Start, End);
            }
            private struct PositionDelta
            {
                private byte Row { get; set; }
                private byte Column { get; set; }
                public PositionDelta(Position end, Position start)
                {
                    Row = (byte)(end.Row - start.Row);
                    Column = (byte)(end.Column - start.Column);
                }
                public PositionDelta(int row, int column)
                {
                    Row = (byte)row;
                    Column = (byte)column;
                }
                public PositionDelta(byte row, byte column)
                {
                    Row = row;
                    Column = column;
                }
                public PositionDelta GetUnitDelta()
                {
                    return new((this.Row == 0) ? 0 : 1, (this.Column == 0) ? 0 : 1);
                }
                public static PositionDelta operator *(int i, PositionDelta pd) => new((pd.Row * i), (pd.Column * i));
                public static PositionDelta operator *(PositionDelta pd, int i) => new((pd.Row * i), (pd.Column * i));

                public static Position operator +(Position p, PositionDelta pd) => new(p.Row + pd.Row, p.Column + pd.Column);
            }

        }
    }
}
