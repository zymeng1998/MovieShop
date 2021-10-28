using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    // all action methods shold work for authenticated users.
    public class UserController : Controller
    {
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
        public async Task<IActionResult> Purchases(int Id) {
            // list of moviecard
            return View();
        }

        public async Task<IActionResult> Favorites(int Id) {
            // all movies favorited by that user
            return View();
        }

        public async Task<IActionResult> Reviews(int Id) {
            // all reviews
            return View();
        }
    }
}
