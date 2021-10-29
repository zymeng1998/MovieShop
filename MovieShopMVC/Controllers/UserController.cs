﻿using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    // all action methods shold work for authenticated users.
    public class UserController : Controller
    {
        private readonly IUserServices _userService;
        private readonly ICurrentUserService _currentUserService;

        public UserController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public UserController(IUserServices userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Purchase() {
            // 
            //purchase when user click on purchase button in movie details
            return View();                        
        }

        [HttpPost]
        public async Task<IActionResult> Favorite() {
            // favorite a movie when clicks on favorite button in movie details
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Review() {
            // add a review to the movie
            return View();
        }
        // filter in asp.net
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Purchases() {
            // list of moviecard
            //int uid = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            // call get a list of moviecard

            // dbconext in the repo
            var uid = _currentUserService.UserId;
            List<MovieCardResponseModel> movieCards = await _userService.GetPurchasesByUserId(uid);
            return View(movieCards);
        }

        public async Task<IActionResult> Favorites() {
            // all movies favorited by that user
            var uid = _currentUserService.UserId;
            List<MovieCardResponseModel> movieCards = await _userService.GetFavoriteByUserId(uid);
            return View(movieCards);
        }

        public async Task<IActionResult> Reviews(int Id) {
            // all reviews
            return View();
        }
    }
}
