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
                var unaryDescriptionsAndLambdas = new List<(string description, UnaryOperationDelegate operation)>()
                {
                    ("Square root", (long factor, long result) => result*result == factor),
                    ("Cube root", (long factor, long result) => result*result*result == factor),
                    ("Second power", (long factor, long result) => factor*factor == result),
                    ("Third power", (long factor, long result) => factor*factor*factor == result)
                };
                var binaryDescriptionsAndLambdas = new List<(string description, BinaryOperationDelegate operation)>()
                {
                    ("Addition", (long a, long b, long result) => a + b == result),
                    ("Subtraction", (long a, long b, long result) => a - b == result),
                    ("Multiplication", (long a, long b, long result) => a * b == result),
                    ("Division", (long a, long b, long result) => a == b * result)
                };
                unaryIdentifiers = new List<UnaryIdentifier>();
                binaryIdentifiers = new List<BinaryIdentifier>();
                otherIdentifiers = new List<ISimpleFactorsFormulaIdentifier>();

                foreach (var x in unaryDescriptionsAndLambdas)
                {
                    unaryIdentifiers.Add(new UnaryIdentifier(x.description, x.operation));
                }
                foreach (var x in binaryDescriptionsAndLambdas)
                {
                    binaryIdentifiers.Add(new BinaryIdentifier(x.description, x.operation));
                }
            }
        }
    }
    
}
