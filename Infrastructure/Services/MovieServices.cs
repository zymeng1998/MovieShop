using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Helpers;

namespace Infrastructure.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly IMovieRepository _movieRepository;
        public MovieServices(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetails(int Id)
        {
            var movie = await _movieRepository.GetMovieById(Id);
            if (movie == null)
            {
                throw new Exception($"No Movie found for this {Id}");
            }
            var movieDetails = new MovieDetailsResponseModel()
            {
                Id = movie.Id,
                Budget = movie.Budget,
                Overview = movie.Overview,
                Price = movie.Price,
                PosterUrl = movie.PosterUrl,
                Revenue = movie.Revenue,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                Rating = movie.Rating,
                Tagline = movie.Tagline,
                Title = movie.Title,
                RunTime = movie.RunTime,
                BackdropUrl = movie.BackdropUrl,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl
            };
            foreach (var genre in movie.Genres)
            {
                movieDetails.Genres.Add(
                    new GenreResponseModel
                    {
                        Id = genre.GenreId,
                        Name = genre.Genre.Name
                    });
            }

            foreach (var cast in movie.Casts)
            {
                movieDetails.Casts.Add(
                    new CastResponseModel
                    {
                        Id = cast.CastId,
                        Character = cast.Character,
                        Name = cast.Cast.Name,
                        ProfilePath = cast.Cast.ProfilePath
                    });
            }

            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(
                    new TrailerResponseModel
                    {
                        Id = trailer.Id,
                        MovieId = trailer.MovieId,
                        Name = trailer.Name,
                        TrailerUrl = trailer.TrailerUrl
                    });

            }

            return movieDetails;

        }

        public async Task<List<MovieCardResponseModel>> GetTop30RevenueMovies()
        {
            // method should call moview repo and get the data from movie table

            // calling MovieRepo with DI based on IMovieRepo
            var movies = await _movieRepository.GetTop30RevenueMovies();
            var movieCards = MovieCardHelper.GetMovieCardsFromMovies(movies);
            return movieCards;
        }
    }
}
