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
using Microsoft.AspNetCore.Session;


namespace ClubAtleticoOrt.Controllers
{
    public class ReservasController : Controller
    {
        #region Constantes
        private readonly ClubDatabaseContext _context;
        private const string USUARIO_INVALIDO = "El Dni no está registrado, debe crear un Usuario";
        private const string FECHA_INVALIDA = "La fecha no es valida";
        private const string EXISTE_RESERVA = "Ups... Ya está reservado. Por favor, intente elegir otra cancha o elija un horario diferente";
        #endregion

        public ReservasController(ClubDatabaseContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            ViewBag.Nombre = HttpContext.Session.GetString("nombre");
            ViewBag.Dni = HttpContext.Session.GetString("dni_usuario");
            return View(await _context.Reservas.ToListAsync());
        }

        #region Details
        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewBag.Nombre = HttpContext.Session.GetString("nombre");
            return View(reserva);
        }
        #endregion

        #region Create
        // GET: Reservas/Create
        public IActionResult Create()
        {

            if (HttpContext.Session.GetString("dni_usuario") == null)
            {
                return RedirectToAction("Login", "Acceso");
            }

            ViewBag.Dni = HttpContext.Session.GetString("dni_usuario");
            ViewBag.Nombre = HttpContext.Session.GetString("nombre");

            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,HoraInicio,HoraFin,Nro_cancha")] Reserva reserva)
        {
            bool next = false;
            string dni = HttpContext.Session.GetString("dni_usuario");
            

            if(reserva.Nro_Dni == null)
            {
                next = true;
            }

            if (ModelState.IsValid || next)
            {
                

                try
                {
                    if (!this.validarFecha(reserva.Fecha))
                    {
                        ViewData["Error"] = FECHA_INVALIDA;
                        ViewBag.Dni = HttpContext.Session.GetString("dni_usuario");
                        return View();
                    }
                    else if (this.existeReserva(reserva))
                    {
                        ViewData["Error"] = EXISTE_RESERVA;
                        ViewBag.Dni = HttpContext.Session.GetString("dni_usuario");
                        return View();
                    }
                    else 
                    {
                        reserva.Nro_Dni = dni;
                        _context.Add(reserva);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
                }
            }
            return View(reserva);
        }
        #endregion

        #region Edit
        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Reserva reserva = await _context.Reservas.FindAsync(id);
            string dniUsuarioReserva = reserva.Nro_Dni;

            if (HttpContext.Session.GetString("dni_usuario") == null)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else if(HttpContext.Session.GetString("dni_usuario") != dniUsuarioReserva)
            {
                return RedirectToAction("Index", "Reservas");
            }
            

            if (id == null)
            {
                return NotFound();
            }

            
            
            if (reserva == null)
            {
                return NotFound();
            }
            ViewBag.Nombre = HttpContext.Session.GetString("nombre");
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,HoraInicio,HoraFin,Nro_cancha")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

           /* if (!this.validarUsuario(reserva.Nro_Dni))
            {
                ViewData["Error"] = USUARIO_INVALIDO;
                return View();
            }*/

            bool next = false;
            string dni = HttpContext.Session.GetString("dni_usuario");


            if (reserva.Nro_Dni == null)
            {
                next = true;
            }


            if (ModelState.IsValid || next)
            {
                try
                {
                    if (!this.validarFecha(reserva.Fecha))
                    {
                        ViewData["Error"] = FECHA_INVALIDA;
                        return View();
                    }
                    else if (this.existeReserva(reserva))
                    {
                        ViewData["Error"] = EXISTE_RESERVA;
                        return View();
                    }
                    else 
                    {
                        //Eliminar de la DB la reserva original, antes de añadir la nueva
                       // await this.DeleteConfirmed(id);

                        reserva.Nro_Dni = dni;
                        _context.Update(reserva);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(reserva);
        }
        #endregion

        #region Delete
        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Reserva reserva = await _context.Reservas.FindAsync(id);
            string dniUsuarioReserva = reserva.Nro_Dni;

            if (HttpContext.Session.GetString("dni_usuario") == null)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else if (HttpContext.Session.GetString("dni_usuario") != dniUsuarioReserva)
            {
                return RedirectToAction("Index", "Usuario");
            }

            if (id == null)
            {
                return NotFound();
            }

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Métodos_Auxiliares

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }


        private bool validarFecha(DateTime fecha)
        {
            //var fechaActual = DateTime.Now;

            return fecha > DateTime.Now;
        }

        private bool existeReserva(Reserva reserva)  {
            bool encontrado = false;
            var reservasPorCancha = (from d in _context.Reservas
                                     where d.Fecha.DayOfYear == reserva.Fecha.DayOfYear 
                                        && d.HoraInicio == reserva.HoraInicio
                                        && d.Nro_cancha == reserva.Nro_cancha
                                     select d.HoraInicio).ToList();
            if (reservasPorCancha.Count() > 0)
            {
                encontrado = true; 
            }
            return encontrado;
        }


        private bool validarUsuario(string dni)
        {
            return _context.Usuarios.Any(e => e.Dni == dni);
        }

        #endregion
    }
}
