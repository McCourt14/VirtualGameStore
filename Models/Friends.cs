using System;
using System.Collections.Generic;

namespace VirtualGameStore.Models
{
    public partial class Friends
    {
        public decimal Friendid { get; set; }
        public decimal? Userid { get; set; }
        public decimal? FriendUserid { get; set; }
        public decimal? UpdatedUserid { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }

        public User FriendUser { get; set; }
        public User User { get; set; }
    }
}
