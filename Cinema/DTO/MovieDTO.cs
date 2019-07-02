using Cinema.Models.Tickets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.DTO
{
    public class MovieDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(8000)]
        public string Description { get; set; }

        public int Duration { get; set; }

        [Range(typeof(float),"0.0","10.0")]
        public float Rating { get; set; }

        public ICollection<TypeDTO> Types { get; set; }

        public ICollection<GenreDTO> Genres { get; set; }
        
        public int MinAge { get; set; }

        [MaxLength(400)]
        public string ImageUrl { get; set; }

        public MovieDTO()
        {
            Types = new List<TypeDTO>();
            Genres = new List<GenreDTO>();
        }
    }
}