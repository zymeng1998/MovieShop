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
    public class MovieRepository : IMovieRepository
    {
        public MovieShopDbContext _dbContext;
        public MovieRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
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
