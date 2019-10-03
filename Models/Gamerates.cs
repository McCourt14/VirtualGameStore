using System;
using System.Collections.Generic;

namespace VirtualGameStore.Models
{
    public partial class Gamerates
    {
        public decimal Rateid { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Gameid { get; set; }
        public decimal? Rates { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public Game Game { get; set; }
        public User User { get; set; }
    }
}
