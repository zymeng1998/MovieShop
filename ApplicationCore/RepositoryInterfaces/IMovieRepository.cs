using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository
    {
        // method that is gonna get 30 highest revenue movies
        Task<IEnumerable<Movie>> GetTop30RevenueMovies();
        Task<Movie> GetMovieById(int Id);
    }
}
