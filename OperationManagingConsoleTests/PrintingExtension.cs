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
        public static void Print<T>(this (T,T) tuple)
        {
            Console.WriteLine("{0}; {1}", tuple.Item1, tuple.Item2);
        }
        public static void Print(this IEnumerable<Digit> line)
        {
            foreach (var digit in line)
            {
                Console.Write("{0} ", digit);
            }
            Console.WriteLine();
        }
        public static void PrintColumnInfo(int count)
        {
            Console.Write("Column: ");
            for (int i = 0; i < count; i++)
            {
                Console.Write("{0} ", i.ToString("D2"));
            }
            Console.WriteLine();
        }
        public static void PrintSeparator()
        {

            Console.WriteLine("-------------------------------------------------");
        }
        public static void Print(this Digit?[][] board)
        {
            PrintSeparator();
            PrintColumnInfo(board[0].Length);

            int index = 0;
            foreach (var row in board)
            {
                Console.Write("row {0}:|", index.ToString("D2"));
                foreach (var digit in row)
                {
                    Console.Write(digit == null ? "__" : "_"+digit);
                    Console.Write("|");
                }
                Console.Write("{0} row", index.ToString("D2"));
                Console.WriteLine();
                index++;
            }
            PrintColumnInfo(board[0].Length);
            PrintSeparator();
        }
    }
}
