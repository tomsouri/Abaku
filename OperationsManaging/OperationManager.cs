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



        private class OperationSetupTool : ISetupTool
        {
            public string Description => throw new NotImplementedException();

            public void Setup()
            {
                throw new NotImplementedException();
            }
        }

        public IReadOnlyList<ISetupTool> GetOperationSetupTools()
        {
            throw new NotImplementedException();
        }

    }
}
