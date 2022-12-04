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
        private const string NOMBRE = "Nombre";
        private const string APELLIDO = "Apellido";
        private const string DNI = "Dni";
        private const string CONTRASEÑA = "Contraseña";
        private const string EMAIL = "Email";
        private const string TELEFONO = "Telefono";

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
            bool EmailExistente = BuscarDatoUsuario(EMAIL, usuarios);
            bool DniExistente = BuscarDatoUsuario(DNI, usuarios);

            if (ModelState.IsValid && !EmailExistente && !DniExistente)
            {
                usuario.Nombre = Request.Form[NOMBRE];
                usuario.Apellido = Request.Form[APELLIDO];
                usuario.Email = Request.Form[EMAIL];
                usuario.Contraseña = Request.Form[CONTRASEÑA];
                usuario.FechaInscripto = DateTime.Now;
                usuario.Telefono = Request.Form[TELEFONO];

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
                if (lista[i].Email == Request.Form[campo] || lista[i].Dni == Request.Form[campo])
                {
                    encontrado = true;
                    ViewData["DatoExistente"] = $"El {campo} ya se encuentra registrado";
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
