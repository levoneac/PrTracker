﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.EventArguments
{
    public class GraphCategoryChangeArgs : EventArgs
    {
        public bool IsOneRepMax { get; private set; }
        public string LiftName { get; private set; }
        public bool IsStrictlyIncreasing { get; private set; }
        public GraphCategoryChangeArgs(bool isOneRepMax, string liftName, bool isStrictlyIncreasing)
        {
            IsOneRepMax = isOneRepMax;
            LiftName = liftName;
            IsStrictlyIncreasing = isStrictlyIncreasing;
        }
    }
}
