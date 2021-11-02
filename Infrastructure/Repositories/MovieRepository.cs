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
    }
}
