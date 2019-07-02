using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cinema.DTO
{
    public class TimeslotDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        [ForeignKey("MovieDTO")]
        public int MovieId { get; set; }

        [Required]
        [ForeignKey("HallDTO")]
        public int HallId { get; set; }

        [Required]
        [ForeignKey("TariffDTO")]
        public int TariffId { get; set; }

        public virtual MovieDTO MovieDTO { get; set; }
        public virtual HallDTO HallDTO { get; set; }
        public virtual TariffDTO TariffDTO { get; set; }


    }
}