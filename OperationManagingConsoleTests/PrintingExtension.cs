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
            Console.Write("Column: ");
            for (int i = 0; i < board[0].Length; i++)
            {
                Console.Write("{0} ",i.ToString("D2"));
            }
            Console.WriteLine();
            int index = 0;
            foreach (var row in board)
            {
                Console.Write("row {0}: ", index.ToString("D2"));
                foreach (var digit in row)
                {
                    Console.Write(digit == null ? "__" : " "+digit);
                    Console.Write(" ");

                }
                Console.WriteLine();
                index++;
            }
            Console.WriteLine("--------------------------------------------------");
        }
    }
}
