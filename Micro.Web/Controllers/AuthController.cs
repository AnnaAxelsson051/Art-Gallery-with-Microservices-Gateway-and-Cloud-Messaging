using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Web.Models;
using Micro.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Micro.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
                return View(loginRequestDto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}

