using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using OperationsManaging;
using BoardManaging;

using EnumerableCombineExtensions;
using ListExtensions;

namespace Evaluation
{
    public class EvaluationManager : IEvaluationManager
    {
        public EvaluationManager()
        {
            CurrentFormulaEvaluationDelegate = FormulaEvaluationManager.DefaultFormulaEvaluation;
            CurrentEvaluationBoard = EvaluationBoardManager.DefaultEvaluationBoardInstance;
            CurrentInvalidMoveEvalDelegate = InvalidMoveEvaluationManager.DefaultInvalidMoveEvaluation;
            IsFormulaEvaluationSetDefault = true;
            IsEvaluationBoardSetDefault = true;
            IsInvalidMoveEvaluationSetDefault = true;
        }

        #region FormulaEvaluation
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
        private bool IsFormulaEvaluationSetDefault { get; set; }

        /// <summary>
        /// It's call is valid only once - when the current setting is default.
        /// After it is set up, this method does not do anything.
        /// </summary>
        /// <param name="value">FormulaEvaluationDelegate to set as CurrentFormulaEvaluationDelegate.</param>
        private void SetFormulaEvaluation(FormulaEvaluationDelegate value)
        {
            if (IsFormulaEvaluationSetDefault)
            {
                CurrentFormulaEvaluationDelegate = value;
                IsFormulaEvaluationSetDefault = false;
            }
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
        private bool IsEvaluationBoardSetDefault { get; set; }
        /// <summary>
        /// It's call is valid only once - when the current setting is default.
        /// After it is set up, this method does not do anything.
        /// </summary>
        /// <param name="value">IEvaluationBoard to set as CurrentEvaluationBoard.</param>
        private void SetEvaluationBoard(IEvaluationBoard value)
        {
            if (IsEvaluationBoardSetDefault)
            {
                IsEvaluationBoardSetDefault = false;
                CurrentEvaluationBoard = value;
            }
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
                    score -= digit;
                }
                return score;
            }
            public static IEnumerable<(string, InvalidMoveEvaluationDelegate)> GetDescriptionsAndInvMoveDelegates()
            {
                yield return ("Default invalid move evaluator", DefaultInvalidMoveEvaluation);
            }
        }
        private bool IsInvalidMoveEvaluationSetDefault { get; set; }
        /// <summary>
        /// It's call is valid only once - when the current setting is default.
        /// After it is set up, this method does not do anything.
        /// </summary>
        /// <param name="value">InvalidMoveEvaluationDelegate to set as CurrentInvalidMoveEvaluationDelegate.</param>
        private void SetInvalidMoveEvaluation(InvalidMoveEvaluationDelegate value)
        {
            if (IsInvalidMoveEvaluationSetDefault)
            {
                IsInvalidMoveEvaluationSetDefault = false;
                CurrentInvalidMoveEvalDelegate = value;
            }
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
        /// Finds all formulas included in the move.
        /// </summary>
        /// <param name="move">Current move.</param>
        /// <param name="board">Current board.</param>
        /// <param name="formulaIdentifier">Current IFormulaIdentifier.</param>
        /// <returns>IEnumerable of Formulae (the nested structure).</returns>
        private IEnumerable<Formula> GetAllFormulas(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            var boardAfterMove = new BoardAfterMove(board, move);
            var positions = move.PositionsSorted;
            if (positions.Count == 1)
            {
                var formulas = Enumerable.Empty<Formula>();
                foreach (var direction in Direction.SimpleDirections)
                {
                    formulas = formulas.CombineWith(GetFormulasFromLine(boardAfterMove, positions[0], direction, formulaIdentifier));
                }
                return formulas;
            }
            else
            {
                var formulas = GetFormulasFromLine(boardAfterMove, positions, formulaIdentifier);
                var direction = positions[0].GetDirectionTo(positions[positions.Count - 1]);
                var flippedDirection = direction.Flipped;
                foreach (var position in positions)
                {
                    formulas = formulas.CombineWith(GetFormulasFromLine(boardAfterMove, position, flippedDirection, formulaIdentifier));
                }
                return formulas;
            }
        }

        /// <summary>
        /// Get all formulas that are in the line (row/column), where all the positions are,
        /// such that each formula includes at least one of the positions.
        /// </summary>
        /// <param name="board">BoardAfterMove representing the current state of the board.</param>
        /// <param name="positions">Positions determining the line and to be included.</param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>Enumerable of Formulas.</returns>
        private IEnumerable<Formula> GetFormulasFromLine(BoardAfterMove board,
                                                         IReadOnlyList<Position> positions,
                                                         IFormulaIdentifier formulaIdentifier)
        {
            (Position sectionStart, Position sectionEnd) = board.GetLongestFilledSectionBounds(positions);
            IReadOnlyList<Digit> digits = board.GetSection(sectionStart, sectionEnd);
            int firstPositionIndex = positions[0] - sectionStart;
            var direction = positions[0].GetDirectionTo(positions[positions.Count - 1]);
            var formulas = GetBoundedFormulas(digits,
                                                     board,
                                                     sectionStart,
                                                     direction,
                                                     formulaIdentifier,
                                                     digits.GetSectionsContainingIndex(firstPositionIndex));

            for (int i = 1; i < positions.Count; i++)
            {
                var containedPosition = positions[i];
                var notContainedPosition = positions[i - 1];

                var containedIndex = containedPosition - sectionStart;
                var notContainedIndex = notContainedPosition - sectionStart;

                formulas = formulas.CombineWith(GetBoundedFormulas(
                    digits,
                    board,
                    sectionStart,
                    direction,
                    formulaIdentifier,
                    digits.GetSectionsContainingIndexNotOther(containedIndex, notContainedIndex)));
            }

            return formulas;
        }

        /// <summary>
        /// Get all formulas from digits including the given index.
        /// </summary>
        /// <param name="digits">List of digits, from which we get the formulas.</param>
        /// <param name="board">To be saved in the Formula struct.</param>
        /// <param name="sectionStart">The Position representing 0 index of digits list.</param>
        /// <param name="direction">The direction the line goes.</param>
        /// <param name="formulaIdentifier"></param>
        /// <param name="sectionBounds">Enum of starting and ending indices bounding the formulas.</param>
        /// <returns>Enumerable of Formula structs.</returns>
        private static IEnumerable<Formula> GetBoundedFormulas(IReadOnlyList<Digit> digits,
                                              BoardAfterMove board,
                                              Position sectionStart,
                                              Direction direction,
                                              IFormulaIdentifier formulaIdentifier,
                                              IEnumerable<(int startIndex, int endIndex)> sectionBounds)
        {
            foreach (var (startIndex, endIndex) in sectionBounds)
            {
                var segment = new ReadOnlyListSegment<Digit>(digits, startIndex, endIndex - startIndex + 1);
                if (formulaIdentifier.IsFormula(segment))
                {
                    var startPosition = sectionStart + startIndex * direction;
                    var endPosition = sectionStart + endIndex * direction;
                    yield return new Formula(startPosition, endPosition, board);
                }
            }
        }

        /// <summary>
        /// Get all formulas including the given position, which lie in the given direction on the board after move.
        /// </summary>
        /// <param name="board">BoardAfterMove representing the current state of the board.</param>
        /// <param name="position">Position to be included.</param>
        /// <param name="direction">Direction, combined with position determining the line.</param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>Enumerable of formulas.</returns>
        private static IEnumerable<Formula> GetFormulasFromLine(BoardAfterMove board,
                                                         Position position,
                                                         Direction direction,
                                                         IFormulaIdentifier formulaIdentifier)
        {
            (Position sectionStart, Position sectionEnd) = board.GetLongestFilledSectionBounds(position, direction);
            IReadOnlyList<Digit> digits = board.GetSection(sectionStart, sectionEnd);
            int positionIndex = position - sectionStart;
            return GetBoundedFormulas(digits,
                                             board,
                                             sectionStart,
                                             direction,
                                             formulaIdentifier,
                                             digits.GetSectionsContainingIndex(positionIndex));
        }
        /// <summary>
        /// Evaluates the move in the current situation using also validation.
        /// </summary>
        /// <param name="move">The move to evaluate.</param>
        /// <param name="board">Board - current situation.</param>
        /// <param name="formulaIdentifier">Used IFormulaIdentifier.</param>
        /// <param name="validationDelegate">Validation used to determine, whether the move is evaluated as valid or invalid.</param>
        /// <returns>Score you get for the move, int.</returns>
        int IEvaluationManager.Evaluate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, MoveValidationDelegate validationDelegate)
        {
            if (validationDelegate(move, board, formulaIdentifier))
            {
                return EvaluateValidMoveUnsafe(move, board, formulaIdentifier);
            }
            return EvaluateInvalidMoveUnsafe(move, board, formulaIdentifier);
        }

        /// <summary>
        /// Evaluates the valid move in the current situation using the default formula evaluation delegate.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>The score you get after applying the move.</returns>
        private int EvaluateValidMoveUnsafe(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            return EvaluateValidMoveUnsafe(move, board, formulaIdentifier, CurrentFormulaEvaluationDelegate);
        }

        /// <summary>
        /// Evaluates the invalid move.
        /// </summary>
        /// <param name="move">The move to evaluate.</param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>Score (usually negative) you get after applying the move.</returns>
        private int EvaluateInvalidMoveUnsafe(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            return this.CurrentInvalidMoveEvalDelegate(move);
        }

        /// <summary>
        /// Evaluates a valid move in the current situation.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <param name="formulaEvaluation">The specified delegate used to evaluate each formula.</param>
        /// <returns></returns>
        private int EvaluateValidMoveUnsafe(Move move,
                                      IBoard board,
                                      IFormulaIdentifier formulaIdentifier,
                                      FormulaEvaluationDelegate formulaEvaluation)
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
                return GetAllFormulasIncludedInUnsafe(move, board, formulaIdentifier, CurrentFormulaEvaluationDelegate);
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
        private IEnumerable<FormulaRepresentation> GetAllFormulasIncludedInUnsafe(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, FormulaEvaluationDelegate formulaEvaluation)
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

            /// <summary>
            /// Finds the longest part of a specified row/column (specified by positions to be contained in),
            /// which is already filled (with ignoring vacancy of specified positions). 
            /// </summary>
            /// <param name="ignoreVacancy">Array of those Positions, which should be contained in the section,
            /// and whose vacancy can be ignored.</param>
            /// <returns>The starting and the ending position of the section (as value tuple).</returns>
            public (Position start, Position end) GetLongestFilledSectionBounds(IEnumerable<Position> ignoreVacancy)
            {
                return this.Board.GetLongestFilledSectionBounds(ignoreVacancy);
            }
            /// <summary>
            /// Finds the longest part of a specified row/column 
            /// (specified by a position to be contained in and the direction),
            /// which is already filled (with ignoring vacancy of specified positions). 
            /// </summary>
            /// <param name="toBeContained">Position to be contained. Its vacancy can be ignored.</param>
            /// <param name="direction">The direction specifying row/column.</param>
            /// <returns>Bounds of the section, positions start and end (as value tuple).</returns>
            public (Position start, Position end) GetLongestFilledSectionBounds(Position toBeContained,
                                                                                Direction direction)
            {
                return Board.GetLongestFilledSectionBounds(toBeContained, direction);
            }
            public IReadOnlyList<Digit> GetSection(Position start, Position end)
            {
                return Board.GetSectionAfterApplyingMove(start, end, Move);
            }
        }

        /// <summary>
        /// Simplifying type used for enumeration of all positioned digit in a formula.
        /// </summary>
        private struct Formula : IEnumerable<PositionedDigit>
        {
            public Formula(Position start, Position end, BoardAfterMove boardAfterMove)
            {
                Start = start;
                End = end;
                BoardAfterMove = boardAfterMove;
            }
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
                public static Position operator +(Position p, PositionDelta pd) => new(p.Row + pd.Row, p.Column + pd.Column);
            }

        }
    }
}
