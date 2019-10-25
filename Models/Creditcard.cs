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
