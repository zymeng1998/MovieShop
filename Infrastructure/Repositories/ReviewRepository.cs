using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
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
    }
}
