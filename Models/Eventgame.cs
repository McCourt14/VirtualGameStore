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

        }

        public decimal Eventgameid { get; set; }
        [DisplayName("Event")]
        [Required(ErrorMessage = "Please select Event")]
        public decimal Eventid { get; set; }
        public decimal userid { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public Event Event { get; set; }
    }
}
