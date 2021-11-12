using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User : ControllerBase
    {
        private readonly IUserServices _userService;
        private readonly ICurrentUserService _currentUserService;

        public User(ICurrentUserService currentUserService, IUserServices userService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
        }
        [HttpGet("purchases")]
        [Authorize]
        public async Task<IActionResult> GetUserPurchases()
        {

            var purchases = await _userService.GetAllPurchasesForUser(_currentUserService.UserId);
            return Ok(purchases);
        }
        [Authorize]
        [HttpGet("favorites")]
        public async Task<IActionResult> Favorites()
        {
            // all movies favorited by that user
            var uid = _currentUserService.UserId;
            var favorites = await _userService.GetAllFavoritesForUser(uid);
            return Ok(favorites);
        }
        [Authorize]
        [HttpGet("reviews")]
        public async Task<IActionResult> Reviews()
        {
            // all reviews
            var uid = _currentUserService.UserId;
            var reviews = await _userService.GetAllReviewsByUser(uid);
            return Ok(reviews);
        }
    }
}
