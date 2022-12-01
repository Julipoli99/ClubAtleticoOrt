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
    public class ReservasController : Controller
    {
        private readonly ClubDatabaseContext _context;

        public ReservasController(ClubDatabaseContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservas.ToListAsync());
        }

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

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,HoraInicio,HoraFin,id_cancha")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!this.validarFecha(reserva.Fecha))
                    {
                        ViewData["Error"] = "La fecha no es valida";
                        return View();
                    }
                    else if (this.existeReserva(reserva))
                    {
                        ViewData["Error"] = "Ups... Ya está reservado. Por favor, intente elegir otra cancha o elija un horario diferente";
                        return View();
                    }
                    else 
                    {
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

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reserva reserva = await _context.Reservas.FindAsync(id);
            
            if (reserva == null)
            {
                return NotFound();
            }
            
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,HoraInicio,HoraFin,id_cancha")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!this.validarFecha(reserva.Fecha))
                    {
                        ViewData["Error"] = "La fecha no es valida";
                        return View();
                    }
                    else if (this.existeReserva(reserva))
                    {
                        ViewData["Error"] = "Ups... Ya está reservado. Por favor, intente elegir otra cancha o elija un horario diferente";
                        return View();
                    }
                    else 
                    {
                        //Eliminar de la DB la reserva original, antes de añadir la nueva
                        await this.DeleteConfirmed(id);

                        _context.Add(reserva);
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
                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        //MÉTODOS AUXILIARES

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }


        private bool validarFecha(DateTime fecha)
        {
            var fechaActual = DateTime.Now;

            return fecha > fechaActual;
        }

        private bool existeReserva(Reserva reserva)  {
            bool encontrado = false;
            var reservasPorCancha = (from d in _context.Reservas
                                     where d.Fecha.DayOfYear == reserva.Fecha.DayOfYear 
                                        && d.HoraInicio == reserva.HoraInicio
                                        && d.id_cancha == reserva.id_cancha
                                     select d.HoraInicio).ToList();
            if (reservasPorCancha.Count() > 0)
            {
                encontrado = true; 
            }
            return encontrado;
        }

    }
}
