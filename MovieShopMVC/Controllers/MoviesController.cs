using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieServices _movieService;



        public MoviesController(IMovieServices movieService)
        {
            _movieService = movieService;
        }
        //localhost/movies/details/...
        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var movieDetail = _movieService.GetMovieDetails(Id);
            return View(movieDetail);
        }
    }
}
