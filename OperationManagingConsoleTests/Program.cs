using System;

using OperationsManaging;


namespace OperationManagingConsoleTests
{
    class Program
    {
        static void UniversalFactorsFormulaIdentifierInteractiveTest()
        {
            var uniIfier = new UniversalFactorsFormulaIdentifier();
            Console.WriteLine("Factors formula identifier test.");
            Console.WriteLine("Enter factors to be considered as formula, in format 1,2,4 for factors 1 and 2 and 4.");
            while (true)
            {
                var tokens = Console.ReadLine().Split(",");
                if (tokens[0] == "q") break;
                var factors = new long[tokens.Length];
                for (int i = 0; i < tokens.Length; i++)
                {
                    factors[i] = long.Parse(tokens[i]);
                }
                var isFormula = uniIfier.IsFormula(factors);
                Console.WriteLine(isFormula);
                if (isFormula)
                {
                    Console.WriteLine(uniIfier.GetFormulaString(factors));
                }
                Console.WriteLine("Press Q + Enter to quit this test.");
            }
        }
        static void Main(string[] args)
        {
            UniversalFactorsFormulaIdentifierInteractiveTest();
        }
    }
}
