using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieShopAPI : ControllerBase
    {
        // create an api method that shows top 30 revenue movies
        // so that my single page applications, ios ,androd show those in home screen
        private readonly IMovieServices _movieService;
        public MovieShopAPI(IMovieServices movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll() {
            var movies = await _movieService.GetMovieCardsForHomePage();
            if (!movies.Any())
            {
                // return 404
                return NotFound("No movies found");
            }
            // 200
            return Ok(movies);
        }

        [HttpGet]

        [Route("{id:int}")]
        // https://localhost/api/movies/{id}/
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                // return 404
                return NotFound("No movies found");
            }
            // 200
            return Ok(movie);
        }

        // create tthe api method that shows the top 30 movies
        // json data
        [HttpGet]
        [Route("toprevenue")]
        // attribute based routing
        // https://localhost/api/movies/toprevenue
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTop30RevenueMovies();
            // json data and http status code
            if (!movies.Any())
            {
                // return 404
                return NotFound("No movies found");
            }
            // 200
            return Ok(movies);
            // for converting c# obj to json obj there two ways
            // before .net core 3, we used NewtonSoft.Json library
            // Microsoft created their own Json serialization library
            // system.text.json
        }
    }
}
