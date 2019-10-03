using System;
using System.Collections.Generic;

namespace VirtualGameStore.Models
{
    public partial class Category
    {
        public Category()
        {
            Favorite = new HashSet<Favorite>();
            Game = new HashSet<Game>();
        }

        public decimal Categoryid { get; set; }
        public string Categoriname { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public ICollection<Favorite> Favorite { get; set; }
        public ICollection<Game> Game { get; set; }
    }
}
