using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_theaters.Models
{
    public class TicketModel
    {
        public int Id { get; set; }

        [Required]
        public SeanceModel Seance { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        [StringLength(20, MinimumLength =1)]
        public int Row { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public int Place { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}