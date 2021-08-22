using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
using BoardManaging;
namespace InteractiveConsoleTests
{
    internal static class Common
    {
        public static Digit[] GetDigitsFromHand()
        {
            Console.WriteLine("Enter the digits you have in your hand (separated by space or comma):");
            Digit[] digits = null;
            bool isValid = false;
            while (!isValid)
            {
                isValid = true;
                var tokens = Console.ReadLine().Split(new char[] { ',', ' ' });
                digits = new Digit[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                {
                    isValid &= int.TryParse(tokens[i], out int result);
                    if (result < 0 || result > 9) isValid = false;
                    if (isValid)
                    {
                        digits[i] = (Digit)result;
                    }   
                }
                if (!isValid)
                {
                    Console.WriteLine("Please enter correct digits (that is numbers from 0 to 9), separated by space or comma.");
                }
            }

            return digits;
        }
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
            var ints = new int[] { 6, 3, 9, 8, 1, 1, 2, 7, 2, 9, 2, 7, 9, 7, 2, 6, 8, 2, 1, 0, 2, 1, 4, 5, 7, 6, 1, 6, 3, 9, 5, 1, 4 };
            return new Move(ints.ConvertToDigits(), positions);
        }
        public static Digit[] ConvertToDigits(this int[] ints)
        {
            var digits = new Digit[ints.Length];
            for (int i = 0; i < ints.Length; i++)
            {
                digits[i] = (Digit)ints[i];
            }
            return digits;
        }
        public static Move GetSimpleInitializingMove()
        {
            var positions = new Position[]
            {
                new(7,6),
                new(7,7),
                new(7,8)
            };
            var ints = new int[] { 1, 5, 6 };
            return new Move(ints.ConvertToDigits(), positions);
        }
        public static Move GetProblematicInitMove()
        {
            var positions = new Position[]
            {
                new(3,4),
                new(4,4),
                new(5,4),
                new(6,4),
                new(7,4),
                new(7,0),
                new(7,1),
                new(7,2),
                new(7,3),
                new(7,5),
                new(7,6),
                new(7,7)
            };
            var ints = new int[] { 6, 9, 6, 9, 1, 8, 6, 2, 5, 2, 5, 7 };
            return new Move(ints.ConvertToDigits(), positions);
        }
        public static int ReadNumber()
        {
            var isValid = false;
            int result = 0;
            while (!isValid)
            {
                isValid = int.TryParse(Console.ReadLine(), out result);
                if (!isValid)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
            return result;
        }
        public static Move ReadMoveSimple()
        {
            Console.WriteLine("Enter the starting position:");
            var startPosition = ReadPosition();

            


            throw new NotImplementedException();
        }
        public static Direction ReadDirection()
        {
            Console.WriteLine("Enter the direction (d/down for down, r/right for right):");
            while (true)
            {
                var input = Console.ReadLine();
                switch (input)
                {
                    case "r":
                    case "right":
                        return new Direction(0, 1);
                    case "d":
                    case "down":
                        return new Direction(1, 0);
                    default:
                        break;
                }
                Console.WriteLine("Please, enter a valid identificator of direction, that is r, d, right, or down.");
            }
        }
        public static Move ReadMove()
        {
            Console.WriteLine("How many digits are you going to place?");

            int count = ReadNumber();
            

            var positions = new Position[count];
            var digits = new Digit[count];

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Enter a position (row number, column number, e.i. 2,4 ):");
                positions[i] = ReadPosition();
                Console.WriteLine("Enter the digit placed on this position: ");
                digits[i] = ReadDigit();
            }
            return new Move(digits, positions);
        }
        public static Position ReadPosition()
        {
            var isValid = false;
            int row = 0;
            int column = 0;
            while (!isValid)
            {
                var parts = Console.ReadLine().Split(new char[] { ',', ' ' });
                if (parts.Length == 2)
                {

                    isValid = int.TryParse(parts[0], out row) && int.TryParse(parts[1], out column);
                }
                if (!isValid)
                {
                    Console.WriteLine("Please enter a valid position identificator, that is the row number and the column number, separated by space or by comma, e.i. 2,4 for position with row 2 and column 4.");
                }
            }
            return new Position(row, column);
        }
        public static Digit ReadDigit()
        {
            var isValid = false;
            int result = 0;
            while (!isValid)
            {
                isValid = int.TryParse(Console.ReadLine(), out result);
                if (result < 0 | result > 9) isValid = false;
                if (!isValid)
                {
                    Console.WriteLine("Please enter a valid digit, that is number from 0 to 9.");
                }
            }
            return (Digit)result;
        }
        public static void EnterMove(BoardManager mger)
        {
            mger.EnterMove(Common.ReadMove());
            mger.GetBoardContent().Print();
        }
    }
}
