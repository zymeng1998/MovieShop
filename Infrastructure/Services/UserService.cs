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

namespace Infrastructure.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        public async Task<int> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // check whether email exist
            // 
            var email = requestModel.Email;
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
            var newUser = await _userRepository.AddUser(user);
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
    }
}
