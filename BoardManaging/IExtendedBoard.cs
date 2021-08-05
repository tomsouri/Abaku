using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardManaging
{
    public interface IExtendedBoard :IBoard
    {
        int RowsCount { get; }
        int ColumnsCount { get; }
    }
}
