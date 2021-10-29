using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Helpers
{
    public class MovieCardHelper
    {
        public static MovieCardResponseModel GetMovieCardFromMovie(Movie movie) {
            return new MovieCardResponseModel
            {
                Id = movie.Id,
                PosterUrl = movie.PosterUrl,
                Title = movie.Title
            };
        }

        public static List<MovieCardResponseModel> GetMovieCardsFromMovies(IEnumerable<Movie> movies)
        {
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(GetMovieCardFromMovie(movie));
            }
            return movieCards;
        }
    }
}
