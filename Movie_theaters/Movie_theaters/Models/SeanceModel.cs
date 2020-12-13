using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_theaters.Models
{
    public class SeanceModel
    {
        public int Id { get; set; }

        [Required]
        public TheaterModel Theater { get; set; }

        [Required]
        public CinemaHallModel CinemaHall { get; set; }

        [Required]
        public MovieModel Movie { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
    }
}