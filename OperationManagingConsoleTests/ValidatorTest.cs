using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using BoardManaging;
using OperationsManaging;
using Validation;

namespace InteractiveConsoleTests
{
    internal static class ValidatorTest
    {
        public static void ValidateMove(IValidator validator, IBoard board, IFormulaIdentifier formulaIdentifier)
        {
            var move = Common.ReadMove();
            Console.WriteLine("Is valid: " + validator.IsValid(move,board,formulaIdentifier));
        }
        public static void Test()
        {
            var boardMger = new BoardManager();
            var opMger = new OperationManager();
            var validator = Validator.Instance;

            while (true)
            {
                var quit = false;
                {
                    Console.WriteLine("What do you want to do?");
                    Console.WriteLine("q=quit");
                    Console.WriteLine("e=enter move");
                    Console.WriteLine("v=validate move");
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
                    case "v":
                        ValidateMove(validator, boardMger.Board, opMger.FlaIdentifier);
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
