using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieServices
    {
        Task<List<MovieCardResponseModel>> GetTop30RevenueMovies();
        Task<MovieDetailsResponseModel> GetMovieDetails(int Id);
        Task<List<MovieCardResponseModel>> GetMovieCardsForHomePage();
        Task<List<MovieCardResponseModel>> GetTopRatedMovies();
        Task<IEnumerable<MovieCardResponseModel>> GetMoviesByGenreId(int id);
    }
}
