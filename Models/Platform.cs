using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtualGameStore.Models
{
    public partial class Platform
    {
        public Platform()
        {
            Favorite = new HashSet<Favorite>();
            Game = new HashSet<Game>();
        }

        public decimal Platformid { get; set; }
        [MaxLength(50)]
        public string Platformname { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public ICollection<Favorite> Favorite { get; set; }
        public ICollection<Game> Game { get; set; }
    }
}
