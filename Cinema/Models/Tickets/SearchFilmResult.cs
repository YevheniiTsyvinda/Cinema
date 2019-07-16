using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models.Tickets
{
    public class SearchFilmResult
    {
        public MovieListItem[] Result { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool ShowPaging { get; set; }
    }
}