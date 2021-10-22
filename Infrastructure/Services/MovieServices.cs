using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
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
        private readonly IMovieRepository _movieRepository;
        public MovieServices(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public List<MovieCardResponseModel> GetTop30RevenueMovies()
        {
            // method should call moview repo and get the data from movie table

            // calling MovieRepo with DI based on IMovieRepo
            var movies = _movieRepository.GetTop30RevenueMovies();
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                }); ;
            }
            return movieCards;
        }
    }
}
