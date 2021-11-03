using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EFRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Movie> GetMovieById(int Id)
        {
            var movie = await _dbContext.Movies.Include(m => m.Casts).ThenInclude(cs => cs.Cast)
                .Include(m => m.Genres).ThenInclude(gs => gs.Genre)
                .Include(m => m.Trailers).FirstOrDefaultAsync(m => m.Id == Id);
            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == Id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            if (movieRating > 0) movie.Rating = movieRating;
            return movie;
        }

        public async Task<IEnumerable<Review>> GetMovieReviews(int Id, int pageSize = 30, int page = 1)
        {
            int takes = page * pageSize;
            var reviews = await _dbContext.Reviews.Where(m => m.MovieId == Id).Take(takes).ToListAsync();
            return reviews;
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            var topRatedMovies = await _dbContext.Reviews.Include(m => m.Movie)
                .GroupBy(r => new
                {
                    Id = r.MovieId,
                    r.Movie.PosterUrl,
                    r.Movie.Title,
                    r.Movie.ReleaseDate
                })
                .OrderByDescending(g => g.Average(m => m.Rating))
                .Select(m => new Movie
                {
                    Id = m.Key.Id,
                    PosterUrl = m.Key.PosterUrl,
                    Title = m.Key.Title,
                    ReleaseDate = m.Key.ReleaseDate,
                    Rating = m.Average(x => x.Rating)
                })
                .Take(50)
                .ToListAsync();

            return topRatedMovies;
        }

        // first vs FirstOrDefault
        // Single vs SingleOrDefault
        public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
        {
            // we use EF with LinQ to get top 30 movies by revenue
            // use DbSet to query
            // I?O bound op
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreId(int id, int pageSize, int pageIndex)
        {
            var movieIds = await _dbContext.MovieGenres.Where(g => g.GenreId == id)
                .Select(m => m.MovieId).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            var movies = new List<Movie>();
            foreach (int Id in movieIds)
            {
                movies.Add(await GetMovieById(Id));
            }
            return movies;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            var genres = await _dbContext.Genres.ToListAsync();
            return genres;
        }

        public async Task<Cast> GetCastById(int id)
        {
            var cast = await _dbContext.Cast.FirstOrDefaultAsync(c => c.Id == id);
            return cast;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByCastId(int id)
        {
            var movieIds = await _dbContext.MovieCasts.Where(mc => mc.CastId == id).Select(mc => mc.MovieId).ToListAsync();
            var movies = new List<Movie>();
            foreach (var Id in movieIds)
            {
                movies.Add(await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == Id));
            }
            return movies;
        }
    }
}
