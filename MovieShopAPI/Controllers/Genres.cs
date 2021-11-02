using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Genres : ControllerBase
    {
        private readonly IMovieServices _movieService;

        public Genres(IMovieServices movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllGenres() {
            var responseModel = await _movieService.GetAllGenres();
            if (!responseModel.Any())
            {
                // return 404
                return NotFound("No Genres found");
            }
            // 200
            return Ok(responseModel);
        }


    }
}
