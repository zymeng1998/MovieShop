using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Helpers;
using ApplicationCore.Entities;

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
                return null;
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
        public async Task<List<MovieCardResponseModel>> GetMovieCardsForHomePage() {
            var movies = await _movieRepository.GetAll();
            var moviesDs = movies.Take(45);
            var movieCards = MovieCardHelper.GetMovieCardsFromMovies(moviesDs);
            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.GetTopRatedMovies();
            var movieCards = MovieCardHelper.GetMovieCardsFromMovies(movies);
            return movieCards;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetMoviesByGenreId(int id)
        {
            var movies = await _movieRepository.GetMoviesByGenreId(id);
            var movieCards = MovieCardHelper.GetMovieCardsFromMovies(movies);
            return movieCards;
        }

        public async Task<IEnumerable<MovieReviewResponseModel>> GetReviewByMovieId(int id)
        {
            var reviews = await _movieRepository.GetMovieReviews(id);
            var movie = await _movieRepository.GetMovieById(id);
            List <MovieReviewResponseModel> lst = new List<MovieReviewResponseModel>();
            foreach (var review in reviews)
            {
                lst.Add(new MovieReviewResponseModel {
                    MovieId = id,
                    Name = movie.Title,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText,
                    UserId = review.UserId
                });
            }
            return lst;
        }

        public async Task<IEnumerable<GenreResponseModel>> GetAllGenres()
        {
            var genres = await _movieRepository.GetAllGenres();
            var lst = new List<GenreResponseModel>();
            foreach (var g in genres)
            {
                lst.Add(new GenreResponseModel
                {
                    Id = g.Id,
                    Name = g.Name
                });
            }
            return lst;
        }

        public async Task<Cast> GetCastById(int id)
        {
            var cast = await _movieRepository.GetCastById(id);
            return cast;
        }
    }
}
