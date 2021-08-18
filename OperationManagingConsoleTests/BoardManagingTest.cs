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
        public static Move GetInitializingMove()
        {
            var positions = new Position[]
            {
                new(3,9),
                new(4,9),
                new(4,10),
                new(4,11),
                new(4,12),
                new(4,13),
                new(4,14),
                new(5,7),
                new(5,8),
                new(5,9),
                new(6,7),
                new(6,9),
                new(7,7),
                new(7,8),
                new(7,9),
                new(7,10),
                new(7,11),
                new(7,12),
                new(7,13),
                new(7,14),
                new(8,8),
                new(8,10),
                new(8,12),
                new(9,8),
                new(9,10),
                new(9,12),
                new(10,8),
                new(11,8),
                new(11,9),
                new(11,10),
                new(12,10),
                new(13,10),
                new(14,10)
            };
            var digits = new Digit[]
            {
                6,3,9,8,1,1,2,7,2,9,2,7,9,7,2,6,8,2,1,0,2,1,4,5,7,6,1,6,3,9,5,1,4
            };
            return new Move(digits, positions);
        }
        public static Move ReadMove()
        {
            Console.WriteLine("How many digits are you going to place?");
            int count = int.Parse(Console.ReadLine());

            var positions = new Position[count];
            var digits = new Digit[count];

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Enter a position: ");
                positions[i] = ReadPosition();
                Console.WriteLine("Enter the digit placed on this position: ");
                digits[i] = (Digit)int.Parse(Console.ReadLine());
            }
            return new Move(digits, positions);
        }
        public static Position ReadPosition()
        {
            var parts = Console.ReadLine().Split(new char[] { ',', ' '});
            return new Position(int.Parse(parts[0]), int.Parse(parts[1]));
        }
        public static void EnterMove(BoardManager mger)
        {
            mger.EnterMove(ReadMove());
            mger.GetBoardContent().Print();
        }
        public static void TotalEmptiness(BoardManager mger)
        {
            Console.WriteLine("Is totally empty: {0}", mger.Board.IsEmpty());
        }
        public static void DigitOnPosition(BoardManager mger)
        {
            Console.WriteLine("Enter a position:");
            var pos = ReadPosition();
            Console.WriteLine(mger.Board[pos]);
        }
        public static void ContainsZero(BoardManager mger)
        {
            Console.WriteLine("Enter a position:");
            var pos = ReadPosition();
            Console.WriteLine(mger.Board.ContainsZero(pos));
        }
        public static void OccupiedPositions(BoardManager mger)
        {
            Console.WriteLine("Enter a position:");
            var pos = ReadPosition();
            var occupied = mger.Board.GetAdjacentOccupiedPositions(pos);
            foreach (var oc in occupied)
            {
                Console.WriteLine(oc);
            }

        }
        public static void PositionEmpty(BoardManager mger)
        {
            Console.WriteLine("Enter a position:");
            var pos = ReadPosition();
            Console.WriteLine(mger.Board.IsPositionEmpty(pos));
        }
        public static void PositionStarting(BoardManager mger)
        {
            Console.WriteLine("Enter a position:");
            var pos = ReadPosition();
            Console.WriteLine(mger.Board.IsStartingPosition(pos));
        }
        public static void AdjToOccupied(BoardManager mger)
        {
            Console.WriteLine("Enter a position:");
            var pos = ReadPosition();
            Console.WriteLine(mger.Board.IsAdjacentToOccupiedPosition(pos));
        }
        public static void SectionAfterMove(BoardManager mger)
        {
            Console.WriteLine("Enter starting position:");
            var startPos = ReadPosition();

            Console.WriteLine("Enter ending position:");
            var endingPos = ReadPosition();

            mger.Board.GetSectionAfterApplyingMove(startPos, endingPos, ReadMove()).Print();
        }
        public static void LongestFilledSectionBounds(BoardManager mger)
        {
            Console.WriteLine("Enter first position to be included (and to ignore vacancy):");
            var startPos = ReadPosition();

            Console.WriteLine("Enter second position to be included (and to ignore vacancy):");
            var endingPos = ReadPosition();

            Console.WriteLine("Vacancy of how many other positions should be ignored?");
            int posCount = int.Parse(Console.ReadLine());
            var ignoreVacancy = new Position[posCount];

            for (int i = 0; i < posCount; i++)
            {
                Console.WriteLine("Enter position to ignore vacancy:");
                ignoreVacancy[i] = ReadPosition();
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
                        EnterMove(mger);
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
                        mger.EnterMove(GetInitializingMove());
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
