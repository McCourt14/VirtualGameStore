﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualGameStore.Models
{
    public class Item
    {
        public Game Game
        {
            get;
            set;
        }

        public int Quantity
        {
            get;
            set;
        }
    }
}
