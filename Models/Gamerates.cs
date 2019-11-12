using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VirtualGameStore.Models
{
    public partial class Gamerates
    {
        public decimal Rateid { get; set; }
        [DisplayName("User")]
        public decimal? Userid { get; set; }
        [DisplayName("Game")]
        public decimal? Gameid { get; set; }
        [Range(typeof(decimal), "0", "100", ErrorMessage = "{0} must be a number between {1} and {2}.")]
        [Required]
        public decimal? Rates { get; set; }
        [MaxLength(200)]
        [Required]
        public string Description { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public Game Game { get; set; }
        public User User { get; set; }
    }
}
