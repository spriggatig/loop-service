﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aire.LoopService
{
    public interface IThresholdProvider
    {
        int GetThreshold(DateTime startEnd, DateTime endDate);
    }
}