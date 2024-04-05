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
    public class PagoesController : Controller
    {
        private readonly RecorverNominaContext _context;

        public PagoesController(RecorverNominaContext context)
        {
            _context = context;
        }

        // GET: Pagoes
        public async Task<IActionResult> Index()
        {
            var recorverNominaContext = _context.Pagos.Include(p => p.Empleado);
            var ListaEmpleados = _context.Empleados;
            var LisIngresos = _context.IngresosEmpleados;
            var LisDeducciones = _context.DeduccionesEmpleados;

            var ListaIngresos = new List<IngresosEmpleado>();
            var ListaDeducciones = new List<DeduccionesEmpleado>();
            var ListaPagos = new List<Pago>();

            foreach (var item in LisIngresos)
            {
                ListaIngresos.Add(item);
            }

            foreach (var item in LisDeducciones)
            {
                ListaDeducciones.Add(item);
            }

            foreach (var item in ListaEmpleados)
            {
                decimal Deduccion = ListaDeducciones.Where(x => x.EmpleadoId == item.EmpleadoId).Sum(x => x.Monto);
                decimal Ingreso = ListaIngresos.Where(x => x.EmpleadoId == item.EmpleadoId).Sum(x => x.Monto);
                Empleado dataEmpleado = new Empleado()
                {
                    Nombre = item.Nombre
                };

                Pago NuevoPago = new Pago()
                {
                    EmpleadoId = item.EmpleadoId,
                    PagoId = 6,
                    TotalDeducciones = Deduccion,
                    TotalIngresos = Ingreso,
                    SalarioNeto = item.SalarioBase + Ingreso - Deduccion,
                    Empleado = dataEmpleado,
                    FechaPago = DateTime.Today

                };
                ListaPagos.Add(NuevoPago);
            }

            //return (IActionResult)(ListaPagos);
            return View(ListaPagos);
            //return View(await recorverNominaContext.ToListAsync());
        }

        // GET: Pagoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos
                .Include(p => p.Empleado)
                .FirstOrDefaultAsync(m => m.PagoId == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // GET: Pagoes/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre");
            return View();
        }

        // POST: Pagoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PagoId,EmpleadoId,TotalIngresos,TotalDeducciones,SalarioNeto,FechaPago")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre", pago.EmpleadoId);
            return View(pago);
        }

        // GET: Pagoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre", pago.EmpleadoId);
            return View(pago);
        }

        // POST: Pagoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PagoId,EmpleadoId,TotalIngresos,TotalDeducciones,SalarioNeto,FechaPago")] Pago pago)
        {
            if (id != pago.PagoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagoExists(pago.PagoId))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre", pago.EmpleadoId);
            return View(pago);
        }

        // GET: Pagoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos
                .Include(p => p.Empleado)
                .FirstOrDefaultAsync(m => m.PagoId == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // POST: Pagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago != null)
            {
                _context.Pagos.Remove(pago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.PagoId == id);
        }
    }
}
