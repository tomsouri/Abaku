using CommonTypes;
using System;
using System.Collections.Generic;

namespace OperationsManaging
{
    public class OperationManager : IOperationManager
    {
        public IFormulaIdentifier FormulaIdentifier => throw new NotImplementedException();



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
