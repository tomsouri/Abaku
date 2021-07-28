using System;

namespace OperationsManaging
{
    internal class OperationManager : IOperationManager
    {
        public IFormulaIdentifier FormulaIdentifier => throw new NotImplementedException();

        public IOperationManager GetOperationManager()
        {
            throw new NotImplementedException();
        }
    }
}
