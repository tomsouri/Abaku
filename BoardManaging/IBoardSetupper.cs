using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace BoardManaging
{
    public interface IBoardSetupper
    {
        void Setup(int columns, int rows, Position startPosition);
    }
}
