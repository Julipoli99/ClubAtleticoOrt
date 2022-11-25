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

          //  var canchas = from cancha in _context.Canchas select cancha;

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
                      Cancha cancha = await _context.Canchas.FirstOrDefaultAsync(s => s.Tipo == reserva.id_cancha);
                    if(cancha.Estado == 1)
                    {
                        cancha.Estado = 0;
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

            //Reserva actual = await _context.Reservas.FindAsync(id);

            if (ModelState.IsValid)
            {
                try
                {
                    /*  Cancha canchaAnterior = await _context.Canchas.FirstOrDefaultAsync(s => s.Tipo == reserva.id_cancha);

                      Cancha canchaTipo;

                      if (canchaAnterior.Id == 0)
                      {
                          canchaTipo = canchaAnterior;
                      }
                      else if(canchaAnterior.Id == 1)
                      {
                          canchaTipo = canchaAnterior;
                      }
                      else
                      {
                          canchaTipo = await _context.Canchas.FirstOrDefaultAsync(s => s.Tipo == TipoCancha.CESPED_SINTETICO);
                      }*/

                    // Cancha canchaAnterior = await _context.Canchas.FirstOrDefaultAsync(s => s.Tipo == reserva.id_cancha - 1);

                    // Cancha canchaAnterior = await _context.Canchas.FirstOrDefaultAsync(s => s.Tipo == reserva.id_cancha);



                    // Cancha canchaAnterior2 = await _context.Reservas.FindAsync(id_cancha);

                    Cancha canchaActualizada = await _context.Canchas.FirstOrDefaultAsync(s => s.Tipo == reserva.id_cancha);

                    //Cancha actualizada3 = await _context.Canchas.FirstOrDefaultAsync(s => s.Tipo == actual.id_cancha);

                    // Console.WriteLine(actualizada3);

                  //  Reserva r = new Reserva();

                    //r = await _context.Reservas.FindAsync(id);

                    //Cancha actualizada4 = await _context.Canchas.FirstOrDefaultAsync(s => s.Tipo == r.id_cancha);


                    if (canchaActualizada.Estado == 1)
                    {

                        _context.Update(reserva);
                        await _context.SaveChangesAsync();

                         /*  actualizada4.Estado = 1;                    //NO ESTARIA LEYENDO ESTA PARTE...
                           _context.Canchas.Update(actualizada4);      //
                           await _context.SaveChangesAsync();          //*/

                        canchaActualizada.Estado = 0;
                        _context.Canchas.Update(canchaActualizada);
                        await _context.SaveChangesAsync();
                        

                        

                        
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
            Cancha cancha = await _context.Canchas.FirstOrDefaultAsync(s => s.Tipo == reserva.id_cancha);
            {
                cancha.Estado = 1;
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
