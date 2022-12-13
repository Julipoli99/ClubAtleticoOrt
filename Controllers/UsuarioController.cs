using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClubAtleticoOrt.Context;
using ClubAtleticoOrt.Models;
using Microsoft.AspNetCore.Http;

namespace ClubAtleticoOrt.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ClubDatabaseContext _context;

        #region Constantes
        private const string USUARIO_REGISTRADO = "El Dni ya está registrado";
        #endregion

        public UsuarioController(ClubDatabaseContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            ViewBag.Dni = HttpContext.Session.GetString("dni_usuario");
            return View(await _context.Usuarios.ToListAsync());
        }

        #region Details
        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        #endregion

        #region Create
        // GET: Usuario/Create
        public IActionResult Create()
        {
            return RedirectToAction("Registro", "Acceso");
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Dni,Nombre,Apellido,Email,Contraseña,FechaInscripto,Telefono")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (this.UsuarioExists(usuario.Dni))
                {
                    ViewData["Error"] = "El Dni ya está registrado";
                    return View();
                }
                else if (this.EmailExists(usuario.Email))
                {
                    ViewData["Error"] = "El eMail ya está registrado";
                    return View();
                }
                else
                {
                    usuario.FechaInscripto = DateTime.Now; //Para que la fecha se setee a la actual independientemente de lo que haya colocado el usuario
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return View(usuario);
        }
        #endregion

        #region Edit
        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            string usuarioLogueadoDni = usuario.Dni;

            if (HttpContext.Session.GetString("dni_usuario") == null)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else if (HttpContext.Session.GetString("dni_usuario") != usuarioLogueadoDni)
            {
                return RedirectToAction("Index", "Usuario");
            }


            if (id == null)
            {
                return NotFound();
            }

            
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dni,Nombre,Apellido,Email,Contraseña,FechaInscripto,Telefono")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (this.EmailExists(usuario.Email))
                    {
                        ViewData["Error"] = "El eMail ya está registrado";
                        return View();
                    }
                    else
                    {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(usuario);
        }
        #endregion

        #region Delete
        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            string usuarioLogueadoDni = usuario.Dni;

            if (HttpContext.Session.GetString("dni_usuario") == null)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else if (HttpContext.Session.GetString("dni_usuario") != usuarioLogueadoDni)
            {
                return RedirectToAction("Index", "Usuario");
            }


            if (id == null)
            {
                return NotFound();
            }

            
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Métodos_Auxiliares
        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
        private bool UsuarioExists(string dni)
        {
            return _context.Usuarios.Any(e => e.Dni == dni);
        }
        private bool EmailExists(string eMail)
        {
            return _context.Usuarios.Any(e => e.Email == eMail);
        }

        private bool ValidarUsuario(int id, Usuario usuario)
        {
            var aux = (from d in _context.Usuarios
                        where d.Id == id
                       select d.Dni);
            if (aux.ToString() == usuario.Dni)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
