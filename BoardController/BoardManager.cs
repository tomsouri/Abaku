using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardController
{
    namespace BoardManager
    {

        internal class BoardManager : IBoardManager
        {
            public IBoard Board => throw new NotImplementedException();
        }
    }

}
