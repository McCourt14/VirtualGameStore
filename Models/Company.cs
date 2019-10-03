using System;
using System.Collections.Generic;

namespace VirtualGameStore.Models
{
    public partial class Company
    {
        public Company()
        {
            Game = new HashSet<Game>();
        }

        public decimal Companyid { get; set; }
        public decimal Ceoid { get; set; }
        public string CompanyName { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string OfficePhone { get; set; }
        public string OfficePhone1 { get; set; }
        public string OfficePhone2 { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public Person Ceo { get; set; }
        public ICollection<Game> Game { get; set; }
    }
}
