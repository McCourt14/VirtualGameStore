using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VirtualGameStore.Models
{
    public partial class Wishlist
    {
        public decimal Wishlistid { get; set; }
        [DisplayName("User")]
        public decimal? Userid { get; set; }
        [DisplayName("Game")]
        public decimal? Gameid { get; set; }
        [DisplayName("Share with Friends")]
        [Range(typeof(Int32), "0", "1", ErrorMessage = "{0} must be a number between {1}(No) and {2}(Yes).")]
        [MaxLength(1)]
        public decimal? Access { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public Game Game { get; set; }
        public User User { get; set; }
    }
}
