using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VirtualGameStore.Models
{
    public partial class Eventgame
    {
        public Eventgame()
        {
            Order = new HashSet<Order>();
        }

        public decimal Eventgameid { get; set; }
        [DisplayName("Event")]
        [Required(ErrorMessage = "Please select Event")]
        public decimal Eventid { get; set; }
        [Required(ErrorMessage = "Please select Game")]
        [DisplayName("Game")]
        public decimal Gameid { get; set; }
        [Required(ErrorMessage = "Please enter Discount Rate")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Discount Rate; Maximum Two Decimal Points.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Discount Rate; Max 18 digits")]
        [DisplayName("Discount Rate")]
        public decimal DiscountRate { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public Event Event { get; set; }
        public Game Game { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
