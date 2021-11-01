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
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        //private readonly MovieShopDbContext _dbContext;

        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Movie>> GetFavorites(int Id)
        {
            var favorites = await _dbContext.Favorites.Where(u => u.UserId == Id).ToListAsync();
            var movies = new List<Movie>();
            foreach (var fav in favorites)
            {
                var movie = await _dbContext.Movies.FirstAsync(m => m.Id == fav.MovieId);
                movies.Add(movie);
            }
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetPurchases(int Id)
        {
            var purchases = await _dbContext.Purchases.Where(u => u.UserId == Id).ToListAsync();
            var movies = new List<Movie>();
            foreach (var pur in purchases)
            {
                var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == pur.MovieId);
                movies.Add(movie);
            }
            return movies;
        }

        public async Task<IEnumerable<Review>> GetReviewsByUser(int userId)
        {
            var reviews = await _dbContext.Reviews.Where(u => u.UserId == userId).ToListAsync();
            return reviews;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
