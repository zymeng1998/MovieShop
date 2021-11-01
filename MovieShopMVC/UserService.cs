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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            using (var rng = RandomNumberGenerator.Create()) {
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
            if (dbUser == null) {
                throw new Exception("no user found");
            }
            var hashedPassword = GetHashedPassword(requestModel.Password, dbUser.Salt);
            if (hashedPassword == dbUser.HashedPassword) {
                // password is correct
                string firstName = "";
                string lastName = "";
                if (dbUser.FirstName != null) firstName = dbUser.FirstName;
                if (dbUser.LastName != null) lastName = dbUser.LastName;
                var userLoginResponseModel = new UserLoginResponseModel {
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

        public Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
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
