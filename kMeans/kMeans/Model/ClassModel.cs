﻿using System.Collections.Generic;
using System.Windows.Media;

namespace kMeans.Model
{
    internal class ClassModel
    {
        internal List<Points> ClassPoints { get; set; }
        internal Color ClassColor { get; set; }
        internal Points CentralPoints { get; set; }
    }
}