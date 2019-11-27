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
        [RegularExpression("^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3" +
            @"(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$", ErrorMessage = "Invalid Credit Card")]
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
