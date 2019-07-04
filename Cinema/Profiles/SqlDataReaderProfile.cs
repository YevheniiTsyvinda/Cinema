using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AutoMapper;
using Cinema.Models.Tickets;
using Type = Cinema.Models.Tickets.Type;

namespace Cinema.Profiles
{
    public class SqlDataReaderProfile : Profile
    {
        public SqlDataReaderProfile()
        {
            CreateMap<SqlDataReader, Movie>()
                .ForMember(x => x.Id, x => x.MapFrom(z => z["Id"]))
                .ForMember(x => x.Name, x => x.MapFrom(z => z["Name"]))
                .ForMember(x => x.Description, x => x.MapFrom(z => z["Description"]))
                .ForMember(x => x.Duration, x => x.MapFrom(z => z["Duration"]))
                .ForMember(x => x.Rating, x => x.MapFrom(z => z["Rating"]))
                .ForMember(x => x.Types, x => x.Ignore())
                .ForMember(x => x.Genres, x => x.Ignore())
                .ForMember(x => x.MinAge, x => x.MapFrom(z => z["MinAge"]))
                .ForMember(x => x.ImageUrl, x => x.MapFrom(z => z["ImageUrl"]))
                .AfterMap((reader, movie) =>
                {
                    var types = (string)reader["Types"];
                    if (!string.IsNullOrEmpty(types))
                    {
                        var parsedTypes = types.Split(',').Select(x=>(Type)Enum.Parse(typeof(Type), x ));
                        movie.Types = parsedTypes.ToArray();
                    }

                    var genres = (string)reader["Genres"];
                    if (!string.IsNullOrEmpty(genres))
                    {
                        var parsedGenres = genres.Split(',').Select(x => (Genre)Enum.Parse(typeof(Genre), x));
                        movie.Genres = parsedGenres.ToArray();
                    }
                });
        }
    }
}