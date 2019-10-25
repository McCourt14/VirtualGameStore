using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace VirtualGameStore.Models
{
    public partial class Favorite
    {
        public decimal Favoritid { get; set; }
        [DisplayName("User")]
        public decimal? Userid { get; set; }
        [DisplayName("Category")]
        public decimal? Categoryid { get; set; }
        [DisplayName("PlatForm")]
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
