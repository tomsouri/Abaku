using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardController;
using CommonTypes;

namespace InteractiveConsoleTests
{
    internal static class BoardControllerTest
    {
        public static void EvaluateAndEnterMove(Move move, IBoardController bc)
        {
            var valid = bc.IsValid(move);
            Console.WriteLine("You entered move: {0}", move);
            Console.WriteLine("Is valid: " + valid);
            Console.WriteLine("Formulas included in: ");
            foreach (var flaRepre in bc.AllFormulasIncludedIn(move))
            {
                Console.WriteLine("{0} ### {1} pts", flaRepre.ToString().PadRight(15), flaRepre.Score);
            }
            Console.WriteLine("----------------------------");
            Console.WriteLine("{0} ### {1} pts", "Score you get:".PadRight(15), bc.Evaluate(move));
            if (valid)
            {
                bc.EnterMove(move);
                bc.GetBoardContent().Print();
            }
        }
        public static void FindBestMove(IBoardController bc)
        {
            var digits = Common.ReadDigits();
            var bestMove = bc.GetBestMove(digits);

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
                    EvaluateAndEnterMove(evalMove.Move,bc);
                }
            }
        }
        public static void Test()
        {
            IBoardController bc = new BoardController.BoardController();
            Console.WriteLine("======= ABAKU =======");
            bc.GetBoardContent().Print();
            while (true)
            {
                var quit = false;
                {
                    Console.WriteLine("What do you want to do?");
                    Console.WriteLine("q = quit");
                    Console.WriteLine("s = enter move with score evaluation");
                    Console.WriteLine("b = find best move");
                }
                var input = Console.ReadLine();
                bc.GetBoardContent().Print();
                switch (input)
                {
                    case "q":
                        quit = true;
                        break;
                    case "s":
                        EvaluateAndEnterMove(Common.ReadMoveSimple(bc.Board), bc);
                        break;
                    case "b":
                        FindBestMove(bc);
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
