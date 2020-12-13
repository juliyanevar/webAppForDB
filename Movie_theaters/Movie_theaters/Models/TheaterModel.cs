using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_theaters.Models
{
    public class TheaterModel
    {
        [Display(Name="ID theater")]
        public int Id { get; set; }

        [Required]
        [Display(Name="Name of theater")]
        public string Name { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        [Display(Name="Count of halls")]
        public int CountHalls { get; set; }
    }
}