using System;
using System.Collections.Generic;

namespace VirtualGameStore.Models
{
    public partial class Person
    {
        public Person()
        {
            Company = new HashSet<Company>();
            User = new HashSet<User>();
        }

        public decimal Psersonid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string OfficePhone { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public ICollection<Company> Company { get; set; }
        public ICollection<User> User { get; set; }
    }
}
