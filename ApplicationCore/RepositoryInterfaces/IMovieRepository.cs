using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        // method that is gonna get 30 highest revenue movies
        Task<IEnumerable<Movie>> GetTop30RevenueMovies();
        Task<IEnumerable<Movie>> GetTopRatedMovies();
        Task<IEnumerable<Movie>> GetMoviesByGenreId(int id);
        Task<Movie> GetMovieById(int Id);
        Task<IEnumerable<Review>> GetMovieReviews(int Id, int pageSize = 30, int page = 1);
    }
}
