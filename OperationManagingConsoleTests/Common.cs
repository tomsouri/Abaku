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
            var parts = Console.ReadLine().Split(new char[] { ',', ' ' });
            return new Position(int.Parse(parts[0]), int.Parse(parts[1]));
        }
        public static void EnterMove(BoardManager mger)
        {
            mger.EnterMove(Common.ReadMove());
            mger.GetBoardContent().Print();
        }
    }
}
