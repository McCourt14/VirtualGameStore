using System;
using System.Collections.Generic;

namespace VirtualGameStore.Models
{
    public partial class Favorite
    {
        public decimal Favoritid { get; set; }
        public decimal? Userid { get; set; }
        public decimal? Categoryid { get; set; }
        public decimal? Platformid { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public Category Category { get; set; }
        public User Favorit { get; set; }
        public Platform Platform { get; set; }
    }
}
