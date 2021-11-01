using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;
using ApplicationCore.Helpers;

namespace Infrastructure.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IPurchaseRepository _purchaseReposotory;
        private readonly IMovieRepository _movieRepository;
        private readonly IReviewRepository _reviewRepository;

        public UserService(IReviewRepository reviewRepository, IUserRepository userRepository, IFavoriteRepository favoriteRepository, IPurchaseRepository purchaseReposotory, IMovieRepository movieRepository)
        {
            _userRepository = userRepository;
            _favoriteRepository = favoriteRepository;
            _purchaseReposotory = purchaseReposotory;
            _movieRepository = movieRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<int> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // check whether email exist
            // 
            string email = requestModel.Email;
            var dbUser = await _userRepository.GetUserByEmail(email);
            if (dbUser != null)
            {
                throw new Exception("Email exists, please login");
            }
            // generate salt
            // create the hashed password
            // save the user object
            var salt = GetSalt();
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);

            // save the user obj to db
            var user = new User
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                DateOfBirth = requestModel.DateOfBirth,
                Email = email,
                Salt = salt,
                HashedPassword = hashedPassword,
            };
            // save to table.
            var newUser = await _userRepository.Add(user);
            return newUser.Id;

        }
        private string GetSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;

        }

        public async Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel)
        {
            // get the salt and hashedpassword for this user
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser == null)
            {
                throw new Exception("no user found");
            }
            var hashedPassword = GetHashedPassword(requestModel.Password, dbUser.Salt);
            if (hashedPassword == dbUser.HashedPassword)
            {
                // password is correct
                string firstName = "";
                string lastName = "";
                if (dbUser.FirstName != null) firstName = dbUser.FirstName;
                if (dbUser.LastName != null) lastName = dbUser.LastName;
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    FirstName = firstName,
                    DateOfBirth = dbUser.DateOfBirth.GetValueOrDefault(),
                    Email = dbUser.Email,
                    Id = dbUser.Id,
                    LastName = lastName
                };
                return userLoginResponseModel;
            }
            return null;
        }

        public async Task<List<MovieCardResponseModel>> GetPurchasesByUserId(int Id)
        {
            var movies = await _userRepository.GetPurchases(Id);
            var movieCards = MovieCardHelper.GetMovieCardsFromMovies(movies);
            return movieCards;

        }

        public async Task<List<MovieCardResponseModel>> GetFavoriteByUserId(int Id)
        {
            var movies = await _userRepository.GetFavorites(Id);
            var movieCards = MovieCardHelper.GetMovieCardsFromMovies(movies);
            return movieCards;
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            await _favoriteRepository.Add(new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId,
                User = await _userRepository.GetById(favoriteRequest.UserId),
                Movie = await _movieRepository.GetById(favoriteRequest.MovieId)
            });
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            await _favoriteRepository.Delete(new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId,
                User = await _userRepository.GetById(favoriteRequest.UserId),
                Movie = await _movieRepository.GetById(favoriteRequest.MovieId)
            });
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            if (await IsMoviePurchased(purchaseRequest, userId))
            {
                return false;
            }
            else
            {
                var movie = await _movieRepository.GetById(purchaseRequest.MovieId);

                await _purchaseReposotory.Add(new Purchase
                {
                    MovieId = purchaseRequest.MovieId,
                    Movie = movie,
                    User = await _userRepository.GetById(userId),
                    UserId = userId,
                    PurchaseNumber = purchaseRequest.PurchaseNumber,
                    PurchaseDateTime = purchaseRequest.PurchaseDateTime,
                    TotalPrice = (decimal)movie.Price
                });
                return true;
            }
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchase = await _purchaseReposotory.GetPurchaseDetails(userId, purchaseRequest.MovieId);
            return purchase != null;
        }

        public async Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId)
        {
            var purchase = await _purchaseReposotory.GetPurchaseDetails(userId, movieId);
            if (purchase == null) throw new Exception("Purchase Record Not Found");
            var movie = await _movieRepository.GetById(movieId);
            PurchaseDetailsResponseModel detailsResponseModel = new PurchaseDetailsResponseModel { 
                Id = purchase.Id,
                MovieId = movieId,
                UserId = userId,
                PosterUrl = movie.PosterUrl,
                PurchaseDateTime = purchase.PurchaseDateTime,
                PurchaseNumber = purchase.PurchaseNumber,
                ReleaseDate = (DateTime)movie.ReleaseDate,
                Title = movie.Title,
                TotalPrice = (decimal)movie.Price
            };
            return detailsResponseModel;
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var movie = await _movieRepository.GetById(reviewRequest.MovieId);
            var user = await _userRepository.GetById(reviewRequest.UserId);
            var review = new Review { 
                Movie = movie,
                MovieId = reviewRequest.MovieId,
                Rating = (decimal)movie.Rating,
                ReviewText = reviewRequest.ReviewText,
                User = user,
                UserId = reviewRequest.UserId
            };
            
        }

        public Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieReview(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<UserReviewResponseModel> GetAllReviewsByUser(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
