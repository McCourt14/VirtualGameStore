using System;
using System.Collections.Generic;

namespace VirtualGameStore.Models
{
    public partial class Creditcard
    {
        public decimal Cardid { get; set; }
        public decimal? Userid { get; set; }
        public string Cardnumber { get; set; }
        public string Cardholder { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public User User { get; set; }
    }
}
