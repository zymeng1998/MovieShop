using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        private readonly IUserServices _userService;
        private readonly IConfiguration _configuration;
        //private readonly ICurrentUserService _currentUserService;

        public Account(IUserServices userService, IConfiguration configuration)
        {
            //_currentUserService = currentUserService;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel requestModel)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Input");
            }
            // save registration to database
            // receive model from view
            var newUser = await _userService.RegisterUser(requestModel);
            // return to login page
            return Ok(newUser);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel requestModel)
        {
            var user = await _userService.LoginUser(requestModel);
            if (user == null)
            {
                return Unauthorized();
            }

            // valid
            // create JWT and send to client (angular), add the claims in token

            return Ok(new { token = GenerateJWT(user) });
        }

        private string GenerateJWT(UserLoginResponseModel responseModel)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, responseModel.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, responseModel.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, responseModel.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, responseModel.LastName),
            };
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));
            // specify the algorithm to sign token
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("ExpirationHours"));
            // creating the token system.identitymodel.tokens.jwt
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _configuration["Issuer"],
                Audience = _configuration["Audience"]
            };
            var encodedJwt = tokenHandler.CreateToken(tokenDesciptor);
            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}
