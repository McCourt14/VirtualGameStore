using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace VirtualGameStore.Models
{
    public partial class Friends
    {
        public decimal Friendid { get; set; }
        [DisplayName("User")]
        public decimal? Userid { get; set; }
        [DisplayName("Friend")]
        public decimal? FriendUserid { get; set; }
        public decimal? UpdatedUserid { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }

        public User FriendUser { get; set; }
        public User User { get; set; }
    }
}
