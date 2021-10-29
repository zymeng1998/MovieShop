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
    public class UserRepository : IUserRepository
    {
        private readonly MovieShopDbContext _dbContext;

        public UserRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<Movie>> GetFavorites(int Id)
        {
            var favorites = _dbContext.Favorites.Where(u => u.UserId == Id).ToList();
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
            var purchases = _dbContext.Purchases.Where(u => u.UserId == Id).ToList();
            var movies = new List<Movie>();
            foreach (var pur in purchases)
            {
                var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == pur.MovieId);
                movies.Add(movie);
            }
            return movies;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
