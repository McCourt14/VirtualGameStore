using System;
using System.Collections.Generic;

namespace VirtualGameStore.Models
{
    public partial class Game
    {
        public Game()
        {
            Eventgame = new HashSet<Eventgame>();
            Gamerates = new HashSet<Gamerates>();
            Wishlist = new HashSet<Wishlist>();
        }

        public decimal Gameid { get; set; }
        public string Title { get; set; }
        public decimal Companyid { get; set; }
        public decimal Price { get; set; }
        public DateTime? LaunchDate { get; set; }
        public decimal? Platformid { get; set; }
        public decimal? Categoryid { get; set; }
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
        public ICollection<Wishlist> Wishlist { get; set; }
    }
}
