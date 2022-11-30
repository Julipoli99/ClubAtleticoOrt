using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAtleticoOrt.Controllers
{
    public class AccesoController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
    }
}
