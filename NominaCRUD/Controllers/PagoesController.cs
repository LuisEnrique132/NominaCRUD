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
        private void CalcularTotalesYNeto(Pago pago)
        {
            // Obtener las deducciones del empleado
            var deduccionesEmpleado = _context.DeduccionesEmpleados.Where(de => de.EmpleadoId == pago.EmpleadoId).ToList();
            decimal totalDeducciones = deduccionesEmpleado.Sum(de => de.Monto);

            // Obtener los ingresos del empleado
            var ingresosEmpleado = _context.IngresosEmpleados.Where(ie => ie.EmpleadoId == pago.EmpleadoId).ToList();
            decimal totalIngresos = ingresosEmpleado.Sum(ie => ie.Monto);

            // Obtener el salario base del empleado
            var empleado = _context.Empleados.FirstOrDefault(e => e.EmpleadoId == pago.EmpleadoId);
            decimal salarioBase = empleado.SalarioBase;

            // Calcular el salario neto
            decimal salarioNeto = salarioBase + totalIngresos - totalDeducciones;

            // Actualizar los campos en el objeto Pago
            pago.TotalDeducciones = totalDeducciones;
            pago.TotalIngresos = totalIngresos;
            pago.SalarioNeto = salarioNeto;
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
        /*  public IActionResult Create()
          {
              var empleados = _context.Empleados.ToList();
              ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "EmpleadoId", "Nombre");
              var nuevoPago = new Pago();

              // Si hay al menos un empleado, calcular los totales y el salario neto del primer empleado
              if (empleados.Any())
              {
                  var primerEmpleado = empleados.First();
                  nuevoPago.EmpleadoId = primerEmpleado.EmpleadoId;


                  CalcularTotalesYNeto(nuevoPago);
              }
              return View(nuevoPago);


          }*/
        // Acción para crear un nuevo pago
        public IActionResult Create(int? empleadoId)
        {
            if (empleadoId == null)
            {
                // Obtener la lista de empleados y sus datos
                var empleados = _context.Empleados.ToList();

                // Crear una lista de selección de empleados
                ViewData["EmpleadoId"] = new SelectList(empleados, "EmpleadoId", "Nombre");

                // Crear un objeto Pago para el formulario de creación
                var nuevoPago = new Pago();

                return View(nuevoPago);
            }
            else
            {
                // Obtener el empleado seleccionado
                var empleadoSeleccionado = _context.Empleados.FirstOrDefault(e => e.EmpleadoId == empleadoId);

                if (empleadoSeleccionado != null)
                {
                    // Crear un objeto Pago para el formulario de creación
                    var nuevoPago = new Pago { EmpleadoId = empleadoSeleccionado.EmpleadoId };

                    // Calcular los totales y el salario neto del empleado seleccionado
                    CalcularTotalesYNeto(nuevoPago);

                    // Asignar la lista de empleados y el empleado seleccionado para el campo de selección
                    ViewData["EmpleadoId"] = new SelectList(_context.Empleados.ToList(), "EmpleadoId", "Nombre", empleadoSeleccionado.EmpleadoId);

                    return View(nuevoPago);
                }
            }

            return NotFound();
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
