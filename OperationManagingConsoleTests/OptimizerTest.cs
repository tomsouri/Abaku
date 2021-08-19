using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using BoardManaging;
using OperationsManaging;
using Validation;
using Evaluation;
using Optimizing;

namespace InteractiveConsoleTests
{
    internal static class OptimizerTest
    {
        public static void FindBestMove(BoardManager boardMger, IFormulaIdentifier formulaIdentifier,
                                        IValidator validator, IEvaluationManager evaluator, IOptimizer optimizer)
        {
            var digits = GetDigitsFromHand();
            var bestMove = optimizer.GetBestMove(digits, (IExtendedBoard)boardMger.Board, formulaIdentifier,
                                                 (IUnsafeEvaluator)evaluator, (IUnsafeValidator)validator);
        }
        public static Digit[] GetDigitsFromHand()
        {
            Console.WriteLine("Enter the digits you have in your hand:");
            var tokens = Console.ReadLine().Split(new char[] { ',', ' ' });
            var digits = new Digit[tokens.Length];
            for (int i = 0; i < tokens.Length; i++)
            {
                digits[i] = (Digit)int.Parse(tokens[i]);
            }
            return digits;
        }
        public static void Test()
        {
            var boardMger = new BoardManager();
            var opMger = new OperationManager();
            var validator = Validator.Instance;
            var evaluator = new EvaluationManager();
            var optimizer = new Optimizer();

            while (true)
            {
                var quit = false;
                {
                    Console.WriteLine("What do you want to do?");
                    Console.WriteLine("q=quit");
                    Console.WriteLine("e=enter any move");
                    Console.WriteLine("s=enter move with evaluation");
                    Console.WriteLine("b=find best move");
                    Console.WriteLine("def = start with default board");
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
                    case "s":
                        EvaluatorTest.EvaluateAndEnterMove(boardMger, opMger.FlaIdentifier, validator, evaluator);
                        break;
                    case "b":

                        break;
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
