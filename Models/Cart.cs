using System;
using System.Collections.Generic;

namespace VirtualGameStore.Models
{
    public partial class Cart
    {
        public decimal? Quantity { get; set; }

        public Game Game { get; set; }
    }
}
