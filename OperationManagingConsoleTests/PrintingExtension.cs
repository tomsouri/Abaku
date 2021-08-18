using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace InteractiveConsoleTests
{
    public static class PrintingExtension
    {
        public static void Print(this Digit?[][] board)
        {
            Console.WriteLine("-------------------------------------------------");
            foreach (var row in board)
            {
                foreach (var digit in row)
                {
                    Console.Write(digit == null ? "_" : digit);
                    Console.Write(" ");

                }
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------");
        }
    }
}
