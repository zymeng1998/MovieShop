using ApplicationCore.ServiceInterfaces;
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
    public class Account : ControllerBase
    {
        private readonly IUserServices _userService;
        private readonly ICurrentUserService _currentUserService;

        public Account(IUserServices userService, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
        }
    }
}
