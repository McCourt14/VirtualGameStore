using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VirtualGameStore.Models
{
    public partial class Game
    {
        public Game()
        {
            Eventgame = new HashSet<Eventgame>();
            Gamerates = new HashSet<Gamerates>();
            Order = new HashSet<Order>();
            Wishlist = new HashSet<Wishlist>();
        }

        public decimal Gameid { get; set; }

        [Required(ErrorMessage = "Please enter Game Title")]
        [MaxLength(50)]
        [DisplayName("Game Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please select Company")]
        [DisplayName("Company")]
        public decimal Companyid { get; set; }

        //[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        [DisplayName("Price")]
        [Required(ErrorMessage = "Please enter Price")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Price; Maximum Two Decimal Points.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Price; Max 18 digits")]
        public decimal Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Launch Date")]
        public DateTime? LaunchDate { get; set; }
        [DisplayName("Platform")]
        public decimal? Platformid { get; set; }
        [DisplayName("Category")]
        public decimal? Categoryid { get; set; }

        [Required(ErrorMessage ="Please enter Game Description")]
        [MaxLength(200)]
        public string Description { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public Category Category { get; set; }

        public Company Company { get; set; }
        public Platform Platform { get; set; }
        public ICollection<Eventgame> Eventgame { get; set; }
        public ICollection<Gamerates> Gamerates { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<Wishlist> Wishlist { get; set; }
    }
}
