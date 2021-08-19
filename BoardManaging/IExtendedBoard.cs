using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;
namespace BoardManaging
{
    public interface IExtendedBoard :IBoard
    {
        int RowsCount { get; }
        int ColumnsCount { get; }
        Position StartingPosition { get; }
    }
}
