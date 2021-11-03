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
    public class Cast : ControllerBase
    {
        private readonly IMovieServices _movieService;

        public Cast(IMovieServices movieService)
        {
            _movieService = movieService;
        }
        // wrong, get the movies for that cast
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMoviesByCastId(int id)
        {
            var responseModel = await _movieService.GetMoviesByCastId(id);
            if (responseModel == null)
            {
                // return 404
                return NotFound("No such a cast found");
            }
            // 200
            return Ok(responseModel);
        }
    }
}
