﻿using System;

namespace BoardManaging
{
    internal class BoardManager : IBoardManager
    {
        public IBoard Board => throw new NotImplementedException();

        public IBoardManager GetBoardManager()
        {
            throw new NotImplementedException();
        }
    }
}
