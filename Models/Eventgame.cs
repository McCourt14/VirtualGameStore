using System;
using System.Collections.Generic;

namespace VirtualGameStore.Models
{
    public partial class Eventgame
    {
        public decimal Eventgameid { get; set; }
        public decimal Eventid { get; set; }
        public decimal Gameid { get; set; }
        public decimal DiscountRate { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public Event Event { get; set; }
        public Game Game { get; set; }
    }
}
