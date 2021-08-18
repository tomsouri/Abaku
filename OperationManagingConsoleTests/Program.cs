using System;

using CommonTypes;

namespace InteractiveConsoleTests
{

    class Program
    {

        static void OpManaging()
        {
            OperationManagingTest.UniversalFactorsFormulaIdentifierInteractiveTest();
            OperationManagingTest.FormulaIdentifierInteractiveTest();
        }

        static void CommonTypesTest()
        {
            var pos = new Position(4, 6);
            Console.WriteLine(pos);
            var move = new Move(new Digit[] { (Digit)1, (Digit)3, (Digit)4, (Digit)5 }, new Position[] { new(5, 6), new(5, 7), new(5, 8), new(5, 9) });
            Console.WriteLine(move);
            var dir = pos.GetDirectionTo(new(4, 8));
            Console.WriteLine(dir);
        }
        static void Main(string[] args)
        {
            //OpManaging();
            CommonTypesTest();
        }
    }
}
