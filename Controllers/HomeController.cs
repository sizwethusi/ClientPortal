using ClientPortalWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using Microsoft.EntityFrameworkCore;
using ClientPortalWeb.Data;

namespace ClientPortalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
