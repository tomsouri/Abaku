using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using OperationsManaging;
using BoardManaging;
using Validation;
using Evaluation;

namespace InteractiveConsoleTests
{
    internal static class EvaluatorTest
    {
        public static void EvaluateMove(BoardManager boardMger, IFormulaIdentifier formulaIdentifier, IValidator validator,
                                    IEvaluationManager evaluator)
        {
            var move = Common.ReadMove();
            Console.WriteLine("Is valid: " + validator.IsValid(move, boardMger.Board, formulaIdentifier));
            Console.WriteLine("Score you get: " + evaluator.Evaluate(move,boardMger.Board,formulaIdentifier,validator.IsValid));
        }
        public static void EvaluateAndEnterMove(BoardManager boardMger, IFormulaIdentifier formulaIdentifier, IValidator validator,
                            IEvaluationManager evaluator)
        {
            var move = Common.ReadMove();
            var valid = validator.IsValid(move, boardMger.Board, formulaIdentifier);
            Console.WriteLine("Is valid: " + valid);
            Console.WriteLine("Score you get: " + evaluator.Evaluate(move, boardMger.Board, formulaIdentifier, validator.IsValid));

            if (valid)
            {
                boardMger.EnterMove(move);
            }
        }
        public static void Test()
        {
            var boardMger = new BoardManager();
            var opMger = new OperationManager();
            var validator = Validator.Instance;
            var evaluator = new EvaluationManager();

            while (true)
            {
                var quit = false;
                {
                    Console.WriteLine("What do you want to do?");
                    Console.WriteLine("q=quit");
                    Console.WriteLine("e=enter move");
                    Console.WriteLine("v=validate move");
                    Console.WriteLine("s=evaluate score of move");
                    Console.WriteLine("def = start with default board");
                    Console.WriteLine("i=infinite loop in evaluating and entering moves");
                }

                var input = Console.ReadLine();
                boardMger.GetBoardContent().Print();
                switch (input)
                {
                    case "q":
                        quit = true;
                        break;
                    case "e":
                        Common.EnterMove(boardMger);
                        break;
                    case "v":
                        ValidatorTest.ValidateMove(validator, boardMger.Board, opMger.FlaIdentifier);
                        break;
                    case "s":
                        EvaluateMove(boardMger, opMger.FlaIdentifier, validator, evaluator);
                        break;
                    case "i":
                        while (true)
                        {
                            boardMger.GetBoardContent().Print();
                            EvaluateAndEnterMove(boardMger, opMger.FlaIdentifier,validator,evaluator);
                        }
                    case "def":
                        boardMger = new BoardManager();
                        boardMger.EnterMove(Common.GetInitializingMove());
                        boardMger.GetBoardContent().Print();
                        break;
                    default:
                        break;
                }
                if (quit) break;
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
