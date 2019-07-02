using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.DTO
{
    public class RequestedSeatsDTO
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int Row { get; set; }

        [Required]
        public int Seat { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public int TimeslotId { get; set; }
    }
}