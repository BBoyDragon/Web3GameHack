﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeUtils
{
    public interface IMazeGenerator
    {
        public IMaze Generate(int sizeX, int sizeY);
    }
}

