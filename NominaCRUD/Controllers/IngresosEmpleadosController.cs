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
    public class IngresosEmpleadosController : Controller
    {
        private readonly RecorverNominaContext _context;

        public IngresosEmpleadosController(RecorverNominaContext context)
        {
            _context = context;
        }

        // GET: IngresosEmpleados
        public async Task<IActionResult> Index()
        {
            var recorverNominaContext = _context.IngresosEmpleados.Include(i => i.Empleado).Include(i => i.TipoIngreso);
            return View(await recorverNominaContext.ToListAsync());
        }

        // GET: IngresosEmpleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingresosEmpleado = await _context.IngresosEmpleados
                .Include(i => i.Empleado)
                .Include(i => i.TipoIngreso)
                .FirstOrDefaultAsync(m => m.IngresoId == id);
            if (ingresosEmpleado == null)
            {
                return NotFound();
            }

            return View(ingresosEmpleado);
        }

        // GET: IngresosEmpleados/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre");
            ViewData["TipoIngresoId"] = new SelectList(_context.TiposIngresos, "TipoIngresoId", "NombreTipo");
            return View();
        }

        // POST: IngresosEmpleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IngresoId,EmpleadoId,TipoIngresoId,Monto,FechaIngreso")] IngresosEmpleado ingresosEmpleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingresosEmpleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre", ingresosEmpleado.EmpleadoId);
            ViewData["TipoIngresoId"] = new SelectList(_context.TiposIngresos, "TipoIngresoId", "NombreTipo", ingresosEmpleado.TipoIngresoId);
            return View(ingresosEmpleado);
        }

        // GET: IngresosEmpleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingresosEmpleado = await _context.IngresosEmpleados.FindAsync(id);
            if (ingresosEmpleado == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre", ingresosEmpleado.EmpleadoId);
            ViewData["TipoIngresoId"] = new SelectList(_context.TiposIngresos, "TipoIngresoId", "NombreTipo", ingresosEmpleado.TipoIngresoId);
            return View(ingresosEmpleado);
        }

        // POST: IngresosEmpleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IngresoId,EmpleadoId,TipoIngresoId,Monto,FechaIngreso")] IngresosEmpleado ingresosEmpleado)
        {
            if (id != ingresosEmpleado.IngresoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingresosEmpleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngresosEmpleadoExists(ingresosEmpleado.IngresoId))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre", ingresosEmpleado.EmpleadoId);
            ViewData["TipoIngresoId"] = new SelectList(_context.TiposIngresos, "TipoIngresoId", "NombreTipo", ingresosEmpleado.TipoIngresoId);
            return View(ingresosEmpleado);
        }

        // GET: IngresosEmpleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingresosEmpleado = await _context.IngresosEmpleados
                .Include(i => i.Empleado)
                .Include(i => i.TipoIngreso)
                .FirstOrDefaultAsync(m => m.IngresoId == id);
            if (ingresosEmpleado == null)
            {
                return NotFound();
            }

            return View(ingresosEmpleado);
        }

        // POST: IngresosEmpleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingresosEmpleado = await _context.IngresosEmpleados.FindAsync(id);
            if (ingresosEmpleado != null)
            {
                _context.IngresosEmpleados.Remove(ingresosEmpleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngresosEmpleadoExists(int id)
        {
            return _context.IngresosEmpleados.Any(e => e.IngresoId == id);
        }
    }
}
