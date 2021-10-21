using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieServices : IMovieServices
    {
        public List<MovieCardResponseModel> GetTop30RevenueMovies()
        {
            // method should call moview repo and get the data from movie table

           // calling MovieRepo with DI based on IMovieRepo

        }
    }
}
