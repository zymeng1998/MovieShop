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
    public class FavoriteRepository : EFRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbcontext) : base(dbcontext)
        {
            
        }
        public async Task<Favorite> GetByUserIdAndMovieId(int userId, int movieId)
        {
            var favorite = await _dbContext.Favorites.FirstOrDefaultAsync(f => f.MovieId == movieId && f.UserId == userId);
            return favorite;
        }
    }
}
