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
    public class TiposIngresosController : Controller
    {
        private readonly RecorverNominaContext _context;

        public TiposIngresosController(RecorverNominaContext context)
        {
            _context = context;
        }

        // GET: TiposIngresos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposIngresos.ToListAsync());
        }

        // GET: TiposIngresos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposIngreso = await _context.TiposIngresos
                .FirstOrDefaultAsync(m => m.TipoIngresoId == id);
            if (tiposIngreso == null)
            {
                return NotFound();
            }

            return View(tiposIngreso);
        }

        // GET: TiposIngresos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposIngresos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoIngresoId,NombreTipo,Descripcion")] TiposIngreso tiposIngreso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiposIngreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposIngreso);
        }

        // GET: TiposIngresos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposIngreso = await _context.TiposIngresos.FindAsync(id);
            if (tiposIngreso == null)
            {
                return NotFound();
            }
            return View(tiposIngreso);
        }

        // POST: TiposIngresos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoIngresoId,NombreTipo,Descripcion")] TiposIngreso tiposIngreso)
        {
            if (id != tiposIngreso.TipoIngresoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposIngreso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposIngresoExists(tiposIngreso.TipoIngresoId))
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
            return View(tiposIngreso);
        }

        // GET: TiposIngresos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposIngreso = await _context.TiposIngresos
                .FirstOrDefaultAsync(m => m.TipoIngresoId == id);
            if (tiposIngreso == null)
            {
                return NotFound();
            }

            return View(tiposIngreso);
        }

        // POST: TiposIngresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiposIngreso = await _context.TiposIngresos.FindAsync(id);
            if (tiposIngreso != null)
            {
                _context.TiposIngresos.Remove(tiposIngreso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposIngresoExists(int id)
        {
            return _context.TiposIngresos.Any(e => e.TipoIngresoId == id);
        }
    }
}
