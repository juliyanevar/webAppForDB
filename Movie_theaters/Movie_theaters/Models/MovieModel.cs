using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_theaters.Models
{
    public class MovieModel
    {
        [Display(Name ="Id movie")]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Age Bracket")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "Minimum 2 symbols")]
        public string AgeBracket { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        [Display(Name ="First day of rental")]
        [DataType(DataType.Date)]
        public DateTime FirstDay { get; set; }

        [Required]
        [Display(Name = "Last day of rental")]
        [DataType(DataType.Date)]
        public DateTime LastDay { get; set; }

        public IList<GenreModel> Genres { get; set; }
    }
}