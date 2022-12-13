using ClubAtleticoOrt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;

using Microsoft.AspNetCore.Mvc.Filters;
//using ClubAtleticoOrt.Permisos;
using Microsoft.AspNetCore.Authorization;

namespace ClubAtleticoOrt.Controllers
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
            var nombreUsuario = HttpContext.Session.GetString("nombre");

            var dni = HttpContext.Session.GetString("dni_usuario");

            ViewBag.Nombre = nombreUsuario;
            ViewBag.Dni = dni;
            
            return View();
        }

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
