using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_theaters.Models
{
    public class GenreModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength =3)]
        public string Name { get; set; }
    }
}