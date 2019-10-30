using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VirtualGameStore.Models
{
    public partial class Creditcard
    {
        public decimal Cardid { get; set; }
        [Required(ErrorMessage = "Please select User")]
        [DisplayName("User")]
        public decimal? Userid { get; set; }
        [Required(ErrorMessage = "Please enter Card Number")]
        [DisplayName("Card Number")]
        [Display(Name = "Credit Card Number")]
        [Range(100000000000, 9999999999999999999, ErrorMessage = "must be between 12 and 19 digits")]
        [MaxLength(50)]
        public string Cardnumber { get; set; }
        [Required(ErrorMessage = "Please enter Card Holder Name")]
        [DisplayName("Holder Name")]
        [MaxLength(50)]
        public string Cardholder { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public User User { get; set; }
    }
}
