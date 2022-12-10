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
            /*  Usuario u = new Usuario();
              if (HttpContext.Session.GetString("usuario") != null)
              {
                  var res = HttpContext.Session.GetString("usuario");
                  u = JsonConvert.DeserializeObject<Usuario>(res);
              }
              else
              {
                  u.Nombre = "NA";
                  u.Apellido = "NA";
                  u.Email = "NA";
                  u.Contraseña = "NA";
                  u.FechaInscripto = DateTime.Today;
                  u.Telefono = "NA";
              }
              ViewBag.Name = HttpContext.Session.GetString("nombre") == null ? "NA" : HttpContext.Session.GetString("nombre").ToString();*/

            

            var nombreUsuario = HttpContext.Session.GetString("nombre");

            ViewBag.Nombre = nombreUsuario;
            
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


       /* public ActionResult mostrarNombre()
        {
            Usuario u = new Usuario();
            if(HttpContext.Session.GetString("usuario") != null)
            {
                var res = HttpContext.Session.GetString("usuario");
                u = JsonConvert.DeserializeObject<Usuario>(res);
            }
            else
            {
                u.Nombre = "NA";
                u.Apellido = "NA";
                u.Email = "NA";
                u.Contraseña = "NA";
                u.FechaInscripto = DateTime.Today;
                u.Telefono = "NA";
            }
            ViewBag["nombre"] = HttpContext.Session.GetString("nombre") == null ? "NA" : HttpContext.Session.GetString("nombre").ToString();
        }

        public ActionResult CerrarSesion()
        {
            HttpContext.Session.Get("usuario").;
        }*/
    }
}
