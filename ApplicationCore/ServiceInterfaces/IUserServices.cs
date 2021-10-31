using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserServices
    {
        // registration
        Task<int> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel);


        // favorite
        Task AddFavorite(FavoriteRequestModel favoriteRequest);
        Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<List<MovieCardResponseModel>> GetFavoriteByUserId(int Id);

        // purchase
        Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
        Task<List<MovieCardResponseModel>> GetPurchasesByUserId(int Id);
        Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId);

        // review
        Task AddMovieReview(ReviewRequestModel reviewRequest);
        Task UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task DeleteMovieReview(int userId, int movieId);
        Task<UserReviewResponseModel> GetAllReviewsByUser(int Id);
    }
}
