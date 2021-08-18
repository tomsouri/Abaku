using System;

using OperationsManaging;
using CommonTypes;

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
        static void FormulaIdentifierInteractiveTest()
        {
            var flaIfier = new FormulaIdentifier(new UniversalFactorsFormulaIdentifier());
            Console.WriteLine("Formula identifier test.");
            Console.WriteLine("Enter string of digits (parted by commas) to be considered as formula and press Enter.");
            while (true)
            {
                var input = Console.ReadLine();
                if (input[0] == 'q') break;

                var digits = new Digit[input.Length];
                for (int i = 0; i < input.Length; i++)
                {
                    digits[i] = (Digit)(int.Parse(input[i].ToString()));
                }                
                
                var isFormula = flaIfier.IsFormula(digits);
                Console.WriteLine(isFormula);
                if (isFormula)
                {
                    Console.WriteLine(flaIfier.GetFormulaString(digits));
                }
                Console.WriteLine("Press Q + Enter to quit this test.");
            }
        }
        static void Main(string[] args)
        {
            UniversalFactorsFormulaIdentifierInteractiveTest();
            FormulaIdentifierInteractiveTest();
        }
    }
}
