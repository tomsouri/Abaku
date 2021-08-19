using CommonTypes;
using System;
using System.Collections.Generic;

using EnumerableCombineExtensions;


namespace OperationsManaging
{
    public class OperationManager : IOperationManager
    {
        public OperationManager()
        {
            this.FactorsIdentifier = new UniversalFactorsFormulaIdentifier();
            this.FlaIdentifier = new FormulaIdentifier(FactorsIdentifier);
        }
        private IUniversalFactorsFormulaIdentifier FactorsIdentifier { get; }
        public IFormulaIdentifier FlaIdentifier { get; }


        private delegate void AddFactorsIdentifierDelegate(ISimpleFactorsFormulaIdentifier identifier);

        private class OperationSetupTool : ISetupTool
        {
            private ISimpleFactorsFormulaIdentifier Identifier { get;}
            private AddFactorsIdentifierDelegate AddDelegate { get; }
            public OperationSetupTool(ISimpleFactorsFormulaIdentifier identifier,
                                      AddFactorsIdentifierDelegate addDelegate)
            {
                Identifier = identifier;
                AddDelegate = addDelegate;
            }
            public string Description => Identifier.Description;

            public void Setup()
            {
                AddDelegate(Identifier);
            }
        }

        public IReadOnlyList<ISetupTool> GetOperationSetupTools()
        {
            var list = new List<ISetupTool>();
            foreach (var identifier in AllOperationsIdentifiers.SimpleIdentifiers)
            {
                list.Add(new OperationSetupTool(identifier, this.FactorsIdentifier.Add));
            }
            return list;
        }


        internal static class AllOperationsIdentifiers
        {
            public static IEnumerable<ISimpleFactorsFormulaIdentifier> SimpleIdentifiers
            {
                get => (UnaryIdentifiers, BinaryIdentifiers, OtherIdentifiers).Combine();
            }
            public static IReadOnlyList<BinaryIdentifier> BinaryIdentifiers => binaryIdentifiers;
            public static IReadOnlyList<UnaryIdentifier> UnaryIdentifiers => unaryIdentifiers;
            public static IReadOnlyList<ISimpleFactorsFormulaIdentifier> OtherIdentifiers => otherIdentifiers;
            private static List<UnaryIdentifier> unaryIdentifiers { get; }
            private static List<BinaryIdentifier> binaryIdentifiers { get; }
            private static List<ISimpleFactorsFormulaIdentifier> otherIdentifiers { get; }
            static AllOperationsIdentifiers()
            {
                var unaryDescriptionsAndLambdas = 
                    new List<(string description, string operationString, UnaryOperationDelegate operation)>()
                {
                    ("Square root", "^1/2", (long factor, long result) => 
                        factor!=0 && result!=0 && result*result == factor),
                    ("Cube root", "^1/3", (long factor, long result) => 
                        factor!=0 && result!=0 && result*result*result == factor),
                    ("Second power", "^2", (long factor, long result) => 
                        factor!=0 && result!=0 && factor*factor == result),
                    ("Third power", "^3", (long factor, long result) =>  
                        factor!=0 && result!=0 && factor*factor*factor == result)
                };
                var binaryDescriptionsAndLambdas = 
                    new List<(string description, string operationString, BinaryOperationDelegate operation)>()
                {
                    ("Addition", "+", (long a, long b, long result) =>
                        a!=0 && b!=0 && result !=0 && a + b == result),
                    ("Subtraction", "-", (long a, long b, long result) =>
                        a!=0 && b!=0 && result !=0 && a - b == result),
                    ("Multiplication", "*", (long a, long b, long result) =>
                        a!=0 && b!=0 && result !=0 && a * b == result),
                    ("Division", "/", (long a, long b, long result) =>
                        a!=0 && b!=0 && result !=0 && a == b * result)
                };
                unaryIdentifiers = new List<UnaryIdentifier>();
                binaryIdentifiers = new List<BinaryIdentifier>();
                otherIdentifiers = new List<ISimpleFactorsFormulaIdentifier>();

                foreach (var (description, operationString, operation) in unaryDescriptionsAndLambdas)
                {
                    unaryIdentifiers.Add(new UnaryIdentifier(description, operation, operationString));
                }
                foreach (var (description, operationString, operation) in binaryDescriptionsAndLambdas)
                {
                    binaryIdentifiers.Add(new BinaryIdentifier(description, operation, operationString));
                }
            }
        }
    }
    
}
