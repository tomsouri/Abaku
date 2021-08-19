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
        public static void TestSequenceGenerator()
        {
            while (true)
            {
                var digits = GetDigitsFromHand();
                var sequences = DigitsSequenceGenerator.GetAllSequences(digits);
                Console.WriteLine("Generated sequences:");
                foreach (var seq in sequences)
                {
                    seq.Print();
                }
                Console.WriteLine("The end of generated sequences.");
            }
        }
        public static void FindBestMove(BoardManager boardMger, IFormulaIdentifier formulaIdentifier,
                                        IValidator validator, IEvaluationManager evaluator, IOptimizer optimizer)
        {
            var digits = GetDigitsFromHand();
            var bestMove = optimizer.GetBestMove(digits, (IExtendedBoard)boardMger.Board, formulaIdentifier,
                                                 (IUnsafeEvaluator)evaluator, (IUnsafeValidator)validator);

            if (bestMove == null)
            {
                Console.WriteLine("There is no valid move in the current situation.");
            }
            else
            {
                var evalMove = (EvaluatedMove)bestMove;
                Console.WriteLine("The best move: ");
                Console.WriteLine(evalMove.Move);
                Console.WriteLine("The score you get for it: {0}", evalMove.Score);
                Console.WriteLine("To enter the move just press Enter... (to skip press X)");
                var input = Console.ReadLine();
                if (input.Length >= 1 && input[0] == 'x')
                {
                    // do nothing
                }
                else
                {
                    EvaluatorTest.EvaluateAndEnterMove(evalMove.Move, boardMger, formulaIdentifier, validator, evaluator);
                }
            }

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
                    Console.WriteLine("d = start with simple default board");
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
                        EvaluatorTest.EvaluateAndEnterMove(Common.ReadMove(),boardMger, opMger.FlaIdentifier, validator, evaluator);
                        break;
                    case "b":
                        FindBestMove(boardMger,opMger.FlaIdentifier, validator,evaluator,optimizer);
                        break;
                    case "def":
                        boardMger = new BoardManager();
                        boardMger.EnterMove(Common.GetInitializingMove());
                        boardMger.GetBoardContent().Print();
                        break;
                    case "d":
                        boardMger = new BoardManager();
                        boardMger.EnterMove(Common.GetSimpleInitializingMove());
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
