using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VirtualGameStore.Models
{
    public partial class Order
    {
        public decimal Orderid { get; set; }
        [DisplayName("Order Date")]
        public DateTime? OrderDate { get; set; }
        [DisplayName("User")]
        public decimal? Userid { get; set; }
        [DisplayName("Order Count")]
        public decimal? OrderCount { get; set; }
        [DisplayName("Order Price")]
        public decimal? OrderPrice { get; set; }
        [DisplayName("Game")]
        public decimal? Gameid { get; set; }
        [Required]
        [DisplayName("Credit card")]
        public decimal? Cardid { get; set; }
        [DisplayName("Discount Rate")]
        public decimal? DiscountRate { get; set; }

        public Eventgame Eventgame { get; set; }
        public Game Game { get; set; }
        public User User { get; set; }
        public Creditcard Card { get; set; }
    }
}
