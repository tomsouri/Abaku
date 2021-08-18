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
        static void BoardManaging()
        {
            BoardManagingTest.BoardMgerTest();
        }
        static void Main(string[] args)
        {
            //OpManaging();
            BoardManaging();
        }
    }
}
