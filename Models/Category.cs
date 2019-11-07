using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please enter Category Name")]
        [MaxLength(50)]
        [DisplayName("Category Name")]
        public string Categoriname { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public decimal? CreatedUserid { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public decimal? UpdatedUserid { get; set; }

        public ICollection<Favorite> Favorite { get; set; }
        public ICollection<Game> Game { get; set; }
    }
}
