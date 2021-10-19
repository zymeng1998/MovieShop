using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using Infrastructure.Services;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        //routing 
        [HttpGet]
        public IActionResult Index()
        {
            //
            MovieServices service = new MovieServices();
            var movieCards = service.GetTop30RevenueMovies();
            // passing data from controller to view, we can use strongly typed models
            // ViewBag and ViewData 
            
            ViewBag.PageTitle = "Top Revenue Movies";
            ViewData["xyz"] = "this is a view data";
            return View(movieCards);
        }
        // https://localhost/home/privacy
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
