﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public struct Move
    {
        (byte, Position)[] PlacedStones;
        public int Evaluation { get; private set; }
        public bool IsEvaluated { get; private set; }
        public Move((byte,Position)[] placedStones)
        {
            PlacedStones = placedStones;
            Evaluation = 0;
            IsEvaluated = false;
        }
    }
}
