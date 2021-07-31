using CommonTypes;
using System;
using System.Collections.Generic;



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
            public static IEnumerable<ISimpleFactorsFormulaIdentifier> SimpleIdentifiers { get; }
            public static IReadOnlyList<BinaryIdentifier> BinaryIdentifiers { get; }
            public static IReadOnlyList<UnaryIdentifier> UnaryIdentifiers { get; }

            // TODO: implement lists of identifiers
        }
    }
}
