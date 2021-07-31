using CommonTypes;
using System;
using System.Collections.Generic;



namespace OperationsManaging
{
    public class OperationManager : IOperationManager
    {
        public OperationManager()
        {
            //this.FormulaIdentifier = 
        }
        public IFormulaIdentifier FormulaIdentifier { get; }



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
