using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            Order = new HashSet<Order>();
            Wishlist = new HashSet<Wishlist>();
        }

        public decimal Userid { get; set; }
        [MaxLength(50)]
        [Required]
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Range(typeof(Int32), "0", "5", ErrorMessage = "{0} must be a number between {1} and {2}.")]
        public decimal? TryCount { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LockDatetime { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Password is too long(Maximum 50)")]
        [MinLength(1, ErrorMessage = "Password required at least 1 character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("User Type")]
        [Required]
        public string Usertype { get; set; }
        [DisplayName("Receive Emails")]
        public decimal? ReceiveEmail { get; set; }
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
        [MaxLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Birth Date")]
        public DateTime? BirthDate { get; set; }
        [MaxLength(10)]
        [DisplayName("Postal Code")]
        public string PostCode { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        [MaxLength(50)]
        public string Province { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(150)]
        [DisplayName("Mailing Address")]
        public string Address { get; set; }
        [MaxLength(150)]
        [DisplayName("Shipping Address")]
        public string Address2 { get; set; }
        [MaxLength(50)]
        public string CellPhone { get; set; }
        [MaxLength(50)]
        public string HomePhone { get; set; }
        [MaxLength(50)]
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
        public ICollection<Order> Order { get; set; }
        public virtual ICollection<Wishlist> Wishlist { get; set; }

        public static implicit operator User(Task<User> v)
        {
            throw new NotImplementedException();
        }
    }

    public class StringRangeAttribute : ValidationAttribute
    {
        public string[] AllowableValues { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowableValues?.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }

            var msg = $"Please enter one of the allowable values: {string.Join(", ", (AllowableValues ?? new string[] { "No allowable values found" }))}.";
            msg += " ("+ErrorMessage+")";
            return new ValidationResult(msg);
        }
    }
}
