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


        private delegate void AddFactorsIdentifierDelegate(IFactorsFormulaIdentifier identifier);

        private class OperationSetupTool : ISetupTool
        {
            private IFactorsFormulaIdentifier Identifier { get;}
            private AddFactorsIdentifierDelegate AddDelegate { get; }
            public OperationSetupTool(string description,
                                      IFactorsFormulaIdentifier identifier,
                                      AddFactorsIdentifierDelegate addDelegate)
            {
                Description = description;
                Identifier = identifier;
                AddDelegate = addDelegate;
            }
            public string Description { get; }

            public void Setup()
            {
                AddDelegate(Identifier);
            }
        }

        public IReadOnlyList<ISetupTool> GetOperationSetupTools()
        {
            throw new NotImplementedException();
        }

    }
}
