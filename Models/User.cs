using System;
using System.Collections.Generic;

namespace VirtualGameStore.Models
{
    public partial class User
    {
        public User()
        {
            Creditcard = new HashSet<Creditcard>();
            Event = new HashSet<Event>();
            FriendsFriendUser = new HashSet<Friends>();
            FriendsUser = new HashSet<Friends>();
            Gamerates = new HashSet<Gamerates>();
            Wishlist = new HashSet<Wishlist>();
        }

        public decimal Userid { get; set; }
        public string DisplayName { get; set; }
        public DateTime StartDate { get; set; }
        public decimal? Personid { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? TryCount { get; set; }
        public DateTime? LockDatetime { get; set; }
        public string Password { get; set; }
        public string Usertype { get; set; }
        public decimal? ReceiveEmail { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public Person Person { get; set; }
        public Favorite Favorite { get; set; }
        public ICollection<Creditcard> Creditcard { get; set; }
        public ICollection<Event> Event { get; set; }
        public ICollection<Friends> FriendsFriendUser { get; set; }
        public ICollection<Friends> FriendsUser { get; set; }
        public ICollection<Gamerates> Gamerates { get; set; }
        public ICollection<Wishlist> Wishlist { get; set; }
    }
}
