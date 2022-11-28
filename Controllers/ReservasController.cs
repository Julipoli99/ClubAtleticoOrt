﻿using System;
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
                      Cancha cancha = await _context.Canchas.FirstOrDefaultAsync(s => s.Id == reserva.id_cancha);
                    if(cancha.Estado == Estado.LIBRE)
                    {
                        cancha.Estado = Estado.RESERVADO;
                        _context.Canchas.Update(cancha);
                        await _context.SaveChangesAsync();

                        _context.Add(reserva);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["Error"] = "El tipo de cancha solicitado ya se encuentra reservado, elige otra";
                        return View();
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

            var reserva = await _context.Reservas.FindAsync(id);
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

                    Cancha canchaActualizada = await _context.Canchas.FirstOrDefaultAsync(s => s.Id == reserva.id_cancha);

                    Reserva actual = await _context.Reservas.FindAsync(id);

                    Cancha anterior = await _context.Canchas.FirstOrDefaultAsync(s => s.Id == actual.id_cancha);

                    Boolean hecho = false;

                    if (canchaActualizada.Estado == Estado.LIBRE)
                    {

                        {

                            canchaActualizada.Estado = Estado.RESERVADO;
                            _context.Canchas.Update(canchaActualizada);
                            await _context.SaveChangesAsync();


                            anterior.Estado = Estado.LIBRE;
                            _context.Canchas.Update(anterior);
                            await _context.SaveChangesAsync();

                            hecho = true;
                        }

                        if (hecho)
                        {
                            _context.Update(reserva);
                            await _context.SaveChangesAsync();
                        }
                       

                    }
                    else
                    {
                        ViewData["Error"] = "El tipo de cancha ya se encuentra reservada, elige otra";
                        return View();
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
            Cancha cancha = await _context.Canchas.FirstOrDefaultAsync(s => s.Id == reserva.id_cancha);
            {
                cancha.Estado = Estado.LIBRE;
            }

            _context.Canchas.Update(cancha);
            await _context.SaveChangesAsync();

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
