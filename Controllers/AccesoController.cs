using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClubAtleticoOrt.Context;
using ClubAtleticoOrt.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;



namespace ClubAtleticoOrt.Controllers
{
    public class AccesoController : Controller
    {

        private readonly ClubDatabaseContext _context;

        public AccesoController(ClubDatabaseContext context)
        {
            _context = context;
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(Usuario usuario)
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            bool EmailExistente = BuscarDatoUsuario("Email", usuarios);
            bool DniExistente = BuscarDatoUsuario("Dni", usuarios);

            if (ModelState.IsValid && !EmailExistente && !DniExistente)
            {
                usuario.Nombre = Request.Form["Nombre"];
                usuario.Apellido = Request.Form["Apellido"];
                usuario.Email = Request.Form["Email"];
                usuario.Contraseña = Request.Form["Contraseña"];
                usuario.FechaInscripto = DateTime.Now;
                usuario.Telefono = Request.Form["Telefono"];

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            EmailExistente = false;
            DniExistente = false;
            return View(usuario);
        }

        private bool BuscarDatoUsuario(string campo, List<Usuario> lista)
        {

            int i = 0;
            bool encontrado = false;

            while (i < lista.Count && !encontrado)
            {
                if (lista[i].Email == Request.Form[campo])
                {
                    encontrado = true;
                    ViewData["DatoExistente"] = "El " + campo + " ya se encuentra registrado";
                }
                else if(lista[i].Dni == Request.Form[campo])
                {
                    encontrado = true;
                    ViewData["DatoExistente"] = "El " + campo + " ya se encuentra registrado";
                }

                else
                {
                    i++;
                }
            }
            return encontrado;
        }

    }
}
