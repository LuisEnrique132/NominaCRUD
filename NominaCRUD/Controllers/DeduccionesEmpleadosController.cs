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
    public class DeduccionesEmpleadosController : Controller
    {
        private readonly RecorverNominaContext _context;

        public DeduccionesEmpleadosController(RecorverNominaContext context)
        {
            _context = context;
        }

        // GET: DeduccionesEmpleados
        public async Task<IActionResult> Index()
        {
            var recorverNominaContext = _context.DeduccionesEmpleados.Include(d => d.Deduccion).Include(d => d.Empleado);
            return View(await recorverNominaContext.ToListAsync());
        }

        // GET: DeduccionesEmpleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deduccionesEmpleado = await _context.DeduccionesEmpleados
                .Include(d => d.Deduccion)
                .Include(d => d.Empleado)
                .FirstOrDefaultAsync(m => m.DeduccionEmpleadoId == id);
            if (deduccionesEmpleado == null)
            {
                return NotFound();
            }

            return View(deduccionesEmpleado);
        }

        // GET: DeduccionesEmpleados/Create
        public IActionResult Create()
        {
            ViewData["DeduccionId"] = new SelectList(_context.Deducciones, "DeduccionId", "DeduccionId");
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "EmpleadoId");
            return View();
        }

        // POST: DeduccionesEmpleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeduccionEmpleadoId,EmpleadoId,DeduccionId,Monto,FechaDeduccion")] DeduccionesEmpleado deduccionesEmpleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deduccionesEmpleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeduccionId"] = new SelectList(_context.Deducciones, "DeduccionId", "DeduccionId", deduccionesEmpleado.DeduccionId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "EmpleadoId", deduccionesEmpleado.EmpleadoId);
            return View(deduccionesEmpleado);
        }

        // GET: DeduccionesEmpleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deduccionesEmpleado = await _context.DeduccionesEmpleados.FindAsync(id);
            if (deduccionesEmpleado == null)
            {
                return NotFound();
            }
            ViewData["DeduccionId"] = new SelectList(_context.Deducciones, "DeduccionId", "DeduccionId", deduccionesEmpleado.DeduccionId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "EmpleadoId", deduccionesEmpleado.EmpleadoId);
            return View(deduccionesEmpleado);
        }

        // POST: DeduccionesEmpleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeduccionEmpleadoId,EmpleadoId,DeduccionId,Monto,FechaDeduccion")] DeduccionesEmpleado deduccionesEmpleado)
        {
            if (id != deduccionesEmpleado.DeduccionEmpleadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deduccionesEmpleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeduccionesEmpleadoExists(deduccionesEmpleado.DeduccionEmpleadoId))
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
            ViewData["DeduccionId"] = new SelectList(_context.Deducciones, "DeduccionId", "DeduccionId", deduccionesEmpleado.DeduccionId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "EmpleadoId", deduccionesEmpleado.EmpleadoId);
            return View(deduccionesEmpleado);
        }

        // GET: DeduccionesEmpleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deduccionesEmpleado = await _context.DeduccionesEmpleados
                .Include(d => d.Deduccion)
                .Include(d => d.Empleado)
                .FirstOrDefaultAsync(m => m.DeduccionEmpleadoId == id);
            if (deduccionesEmpleado == null)
            {
                return NotFound();
            }

            return View(deduccionesEmpleado);
        }

        // POST: DeduccionesEmpleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deduccionesEmpleado = await _context.DeduccionesEmpleados.FindAsync(id);
            if (deduccionesEmpleado != null)
            {
                _context.DeduccionesEmpleados.Remove(deduccionesEmpleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeduccionesEmpleadoExists(int id)
        {
            return _context.DeduccionesEmpleados.Any(e => e.DeduccionEmpleadoId == id);
        }
    }
}
