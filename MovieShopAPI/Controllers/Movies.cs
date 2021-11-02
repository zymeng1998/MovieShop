using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class Movies : ControllerBase
    {
        // create an api method that shows top 30 revenue movies
        // so that my single page applications, ios ,androd show those in home screen
        private readonly IMovieServices _movieService;
        public Movies(IMovieServices movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<IActionResult> GetReviewsByMovieId(int id)
        {
            var reviews = await _movieService.GetReviewByMovieId(id);
            if (!reviews.Any())
            {
                // return 404
                return NotFound("No movies found");
            }
            // 200
            return Ok(reviews);
        }
        [HttpGet]
        [Route("genre/{genreId}")]
        public async Task<IActionResult> GetMoviesByGenreId(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenreId(genreId);
            // json data and http status code
            if (!movies.Any())
            {
                // return 404
                return NotFound("No movies found");
            }
            // 200
            return Ok(movies);
        }
        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTopRatedMovies();
            // json data and http status code
            if (!movies.Any())
            {
                // return 404
                return NotFound("No movies found");
            }
            // 200
            return Ok(movies);
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
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
