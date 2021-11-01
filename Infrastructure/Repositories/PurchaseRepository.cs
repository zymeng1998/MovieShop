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
    public class PurchaseRepository : EFRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }
        public Task<IEnumerable<Purchase>> GetAllPurchases(int pageSize = 30, int pageIndex = 1)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Purchase>> GetAllPurchasesByMovie(int movieId, int pageSize = 30, int pageIndex = 1)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int userId, int pageSize = 30, int pageIndex = 1)
        {
            throw new NotImplementedException();
        }

        public Task<Purchase> GetPurchaseDetails(int userId, int movieId)
        {
            throw new NotImplementedException();
        }
    }
}
