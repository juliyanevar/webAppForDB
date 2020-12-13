using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_theaters.Models
{
    public class CinemaHallModel
    {
        [Display(Name ="Id cinema hall")]
        public int Id { get; set; }

        [Required]
        public TheaterModel Theater { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        [Display(Name ="Count places")]
        public int CountPlaces { get; set; }

        [Required]
        [Display(Name ="Count rows")]
        public int CountRows { get; set; }
    }
}