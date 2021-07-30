using CommonTypes;
using System;
using System.Collections.Generic;

namespace OperationsManaging
{
    public class OperationManager : IOperationManager
    {
        public IFormulaIdentifier FormulaIdentifier => throw new NotImplementedException();

        public IReadOnlyList<ISetupTool> GetOperationSetTools()
        {
            throw new NotImplementedException();
        }
    }
}
