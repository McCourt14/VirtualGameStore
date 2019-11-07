using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VirtualGameStore.Models
{
    public partial class Company
    {
        public Company()
        {
            Game = new HashSet<Game>();
        }

        public decimal Companyid { get; set; }
        [Required(ErrorMessage = "Please enter Ceo Name")]
        [MaxLength(150)]
        public string CeoName { get; set; }
        [Required(ErrorMessage = "Please enter Company Name")]
        [MaxLength(150)]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [MaxLength(10)]
        public string PostCode { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(100)]
        public string Province { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [MaxLength(100)]
        public string Address2 { get; set; }
        [Required(ErrorMessage = "Please enter Office Phone")]
        [MaxLength(20)]
        public string OfficePhone { get; set; }
        [MaxLength(20)]
        public string OfficePhone1 { get; set; }
        [MaxLength(20)]
        public string OfficePhone2 { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public ICollection<Game> Game { get; set; }
    }
}
