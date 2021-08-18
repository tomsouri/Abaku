using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardManaging;
using CommonTypes;

namespace InteractiveConsoleTests
{
    internal static class BoardManagingTest
    {
        public static void TotalEmptiness(BoardManager mger)
        {
            Console.WriteLine("Is totally empty: {0}", mger.Board.IsEmpty());
        }
        public static void DigitOnPosition(BoardManager mger)
        {
            Console.WriteLine("Enter a position:");
            var pos = Common.ReadPosition();
            Console.WriteLine(mger.Board[pos]);
        }
        public static void ContainsZero(BoardManager mger)
        {
            Console.WriteLine("Enter a position:");
            var pos = Common.ReadPosition();
            Console.WriteLine(mger.Board.ContainsZero(pos));
        }
        public static void OccupiedPositions(BoardManager mger)
        {
            Console.WriteLine("Enter a position:");
            var pos = Common.ReadPosition();
            var occupied = mger.Board.GetAdjacentOccupiedPositions(pos);
            foreach (var oc in occupied)
            {
                Console.WriteLine(oc);
            }

        }
        public static void PositionEmpty(BoardManager mger)
        {
            Console.WriteLine("Enter a position:");
            var pos = Common.ReadPosition();
            Console.WriteLine(mger.Board.IsPositionEmpty(pos));
        }
        public static void PositionStarting(BoardManager mger)
        {
            Console.WriteLine("Enter a position:");
            var pos = Common.ReadPosition();
            Console.WriteLine(mger.Board.IsStartingPosition(pos));
        }
        public static void AdjToOccupied(BoardManager mger)
        {
            Console.WriteLine("Enter a position:");
            var pos = Common.ReadPosition();
            Console.WriteLine(mger.Board.IsAdjacentToOccupiedPosition(pos));
        }
        public static void SectionAfterMove(BoardManager mger)
        {
            Console.WriteLine("Enter starting position:");
            var startPos = Common.ReadPosition();

            Console.WriteLine("Enter ending position:");
            var endingPos = Common.ReadPosition();

            mger.Board.GetSectionAfterApplyingMove(startPos, endingPos, Common.ReadMove()).Print();
        }
        public static void LongestFilledSectionBounds(BoardManager mger)
        {
            Console.WriteLine("Enter first position to be included (and to ignore vacancy):");
            var startPos = Common.ReadPosition();

            Console.WriteLine("Enter second position to be included (and to ignore vacancy):");
            var endingPos = Common.ReadPosition();

            Console.WriteLine("Vacancy of how many other positions should be ignored?");
            int posCount = int.Parse(Console.ReadLine());
            var ignoreVacancy = new Position[posCount];

            for (int i = 0; i < posCount; i++)
            {
                Console.WriteLine("Enter position to ignore vacancy:");
                ignoreVacancy[i] = Common.ReadPosition();
            }
            mger.Board.GetLongestFilledSectionBounds((startPos,endingPos), ignoreVacancy).Print();
        }
        public static void BoardMgerTest()
        {
            var mger = new BoardManager();
            while (true)
            {
                var quit = false;
                {
                    Console.WriteLine("What do you want to do?");
                    Console.WriteLine("q=quit");
                    Console.WriteLine("e=enter move");
                    Console.WriteLine("t=ask for total emptiness");
                    Console.WriteLine("d=ask for a digit on a position");
                    Console.WriteLine("z=contains zero");
                    Console.WriteLine("o=get adjacent occupied positions");
                    Console.WriteLine("pe=position empty");
                    Console.WriteLine("s=is starting position");
                    Console.WriteLine("aop = adj to occupied position");
                    Console.WriteLine("sam = section after move");
                    Console.WriteLine("lfs = longest filled section bounds");
                    Console.WriteLine("def = start with default board");
                }

                var input = Console.ReadLine();
                mger.GetBoardContent().Print();
                switch (input)
                {
                    case "q":
                        quit = true;
                        break;
                    case "e":
                        Common.EnterMove(mger);
                        break;
                    case "t":
                        TotalEmptiness(mger);
                        break;
                    case "d":
                        DigitOnPosition(mger);
                        break;
                    case "z":
                        ContainsZero(mger);
                        break;
                    case "o":
                        OccupiedPositions(mger);
                        break;
                    case "pe":
                        PositionEmpty(mger);
                        break;
                    case "s":
                        PositionStarting(mger);
                        break;
                    case "aop":
                        AdjToOccupied(mger);
                        break;
                    case "sam":
                        SectionAfterMove(mger);
                        break;
                    case "lfs":
                        LongestFilledSectionBounds(mger);
                        break;
                    case "def":
                        mger = new BoardManager();
                        mger.EnterMove(Common.GetInitializingMove());
                        mger.GetBoardContent().Print();
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
