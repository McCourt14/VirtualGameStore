using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public decimal? TryCount { get; set; }
        public DateTime? LockDatetime { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Usertype { get; set; }
        public decimal? ReceiveEmail { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string OfficePhone { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public virtual Favorite Favorite { get; set; }
        public virtual ICollection<Creditcard> Creditcard { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<Friends> FriendsFriendUser { get; set; }
        public virtual ICollection<Friends> FriendsUser { get; set; }
        public virtual ICollection<Gamerates> Gamerates { get; set; }
        public virtual ICollection<Wishlist> Wishlist { get; set; }

        public static implicit operator User(Task<User> v)
        {
            throw new NotImplementedException();
        }
    }
}
