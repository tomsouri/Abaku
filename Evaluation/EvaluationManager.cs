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
        /// <summary>
        /// The delegate used for evaluation of formulas.
        /// </summary>
        private FormulaEvaluationDelegate CurrentFormulaEvaluation { get; set; }

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

        /// <summary>
        /// Evaluates the move in the current situation using the default formula evaluation delegate.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>The score you get after applying the move.</returns>
        public int Evaluate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            return Evaluate(move, board, formulaIdentifier, CurrentFormulaEvaluation);
        }

        /// <summary>
        /// Evaluates the move in the current situation.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <param name="formulaEvaluation">The specified delegate used to evaluate each formula.</param>
        /// <returns></returns>
        private int Evaluate(Move move, IBoard board, IFormulaIdentifier formulaIdentifier, FormulaEvaluationDelegate formulaEvaluation)
        {
            int score = 0;
            foreach (var formula in GetAllFormulas(move, board, formulaIdentifier))
            {
                score += formulaEvaluation(formula);
            }
            return score;
        }

        /// <summary>
        /// Uses the defalut formula evaluation delegate to find representations of all formulae and evaluate them.
        /// The expected usage is for displaying all formulae contained in the move with their scores
        /// after applying the move by a player.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="board"></param>
        /// <param name="formulaIdentifier"></param>
        /// <returns>IEnumerable of FormulaRepresentation.</returns>
        public IEnumerable<FormulaRepresentation> GetAllFormulasIncludedIn(Move move, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            return GetAllFormulasIncludedIn(move, board, formulaIdentifier, CurrentFormulaEvaluation);
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

                formulaRepresentation.Score = formulaEvaluation(formula);
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
                    return new((this.Row == 0) ? 0 : 1, (this.Column == 0) ? 0 : 1 );
                }
                public static PositionDelta operator *(int i, PositionDelta pd) => new((pd.Row * i),(pd.Column * i));
                public static PositionDelta operator *(PositionDelta pd, int i) => new((pd.Row * i),(pd.Column * i));

                public static Position operator +(Position p, PositionDelta pd) => new(p.Row + pd.Row, p.Column + pd.Column);


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
