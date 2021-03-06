using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<Movie>> GetPurchases(int Id);
        Task<IEnumerable<Movie>> GetFavorites(int Id);
        Task<IEnumerable<Review>> GetReviewsByUser(int userId);

    }
}
