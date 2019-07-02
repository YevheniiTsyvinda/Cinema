using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.DTO
{
    public class GenreDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public int Name { get; set; }

    }
}