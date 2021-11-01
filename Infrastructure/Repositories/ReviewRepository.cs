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
    public class ReviewRepository : EFRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbcontext) : base(dbcontext)
        {

        }

        public async Task<Review> GetByUserIdAndMovieId(int userId, int movieId)
        {
            var review = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.MovieId == movieId && r.UserId == userId);
            return review;
        }
    
    }
}
