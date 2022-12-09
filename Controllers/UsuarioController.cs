using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClubAtleticoOrt.Context;
using ClubAtleticoOrt.Models;

namespace ClubAtleticoOrt.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ClubDatabaseContext _context;
        private const string USUARIO_REGISTRADO = "El Dni ya está registrado";

        public UsuarioController(ClubDatabaseContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

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

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
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

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
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
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                    /*
                    if (this.ValidarUsuario(id, usuario))//VALIDAR QUE EL DNI DEL USUARIO SEA EL MISMO
                    {
                        //usuario.FechaInscripto = aux.FechaInscripto; //Se verifica que la fecha de inscripcion sea la original
                        var fecha = (from d in _context.Usuarios
                                     where d.Id == id
                                     select d.FechaInscripto).ToString();

                        //usuario.FechaInscripto == DateTime.Parse(fecha);

                        _context.Update(usuario);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["Error"] = "No debe modificarse el Nro de DNI";
                        return View();
                    }
                    */
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

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
        private bool UsuarioExists(string dni)
        {
            return _context.Usuarios.Any(e => e.Dni == dni);
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
    }
}
