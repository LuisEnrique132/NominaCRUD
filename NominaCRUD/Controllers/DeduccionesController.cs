using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NominaCRUD.Models;

namespace NominaCRUD.Controllers
{
    public class DeduccionesController : Controller
    {
        private readonly RecorverNominaContext _context;

        public DeduccionesController(RecorverNominaContext context)
        {
            _context = context;
        }

        // GET: Deducciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Deducciones.ToListAsync());
        }

        // GET: Deducciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deduccione = await _context.Deducciones
                .FirstOrDefaultAsync(m => m.DeduccionId == id);
            if (deduccione == null)
            {
                return NotFound();
            }

            return View(deduccione);
        }

        // GET: Deducciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deducciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeduccionId,NombreDeduccion,Descripcion")] Deduccione deduccione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deduccione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deduccione);
        }

        // GET: Deducciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deduccione = await _context.Deducciones.FindAsync(id);
            if (deduccione == null)
            {
                return NotFound();
            }
            return View(deduccione);
        }

        // POST: Deducciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeduccionId,NombreDeduccion,Descripcion")] Deduccione deduccione)
        {
            if (id != deduccione.DeduccionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deduccione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeduccioneExists(deduccione.DeduccionId))
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
            return View(deduccione);
        }

        // GET: Deducciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deduccione = await _context.Deducciones
                .FirstOrDefaultAsync(m => m.DeduccionId == id);
            if (deduccione == null)
            {
                return NotFound();
            }

            return View(deduccione);
        }

        // POST: Deducciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deduccione = await _context.Deducciones.FindAsync(id);
            if (deduccione != null)
            {
                _context.Deducciones.Remove(deduccione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeduccioneExists(int id)
        {
            return _context.Deducciones.Any(e => e.DeduccionId == id);
        }
    }
}
