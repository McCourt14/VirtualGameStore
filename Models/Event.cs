using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtualGameStore.Models
{
    public partial class Event
    {
        public Event()
        {
            Eventgame = new HashSet<Eventgame>();
        }

        public decimal Eventid { get; set; }
        [MaxLength(100)]
        public string Eventname { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public decimal? RegisterUserid { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public User RegisterUser { get; set; }
        public ICollection<Eventgame> Eventgame { get; set; }
    }
}
