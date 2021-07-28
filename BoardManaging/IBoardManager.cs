using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardManaging
{
    internal interface IBoardManager
    {
        IBoard Board { get; }
    }
}
