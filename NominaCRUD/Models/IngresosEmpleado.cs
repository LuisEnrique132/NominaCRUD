using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NominaCRUD.Models;

public partial class IngresosEmpleado
{
    public int IngresoId { get; set; }

    public int EmpleadoId { get; set; }

    public int TipoIngresoId { get; set; }
    [Display(Name = "Monto total de los Ingresos ")]

    public decimal Monto { get; set; }
    [Display(Name = "Fecha de emision de los Ingresos ")]

    public DateOnly FechaIngreso { get; set; }
    [Display(Name = "Codigo del Empleado")]
    public virtual Empleado Empleado { get; set; } = null!;
    [Display(Name = "Ingreso")]

    public virtual TiposIngreso TipoIngreso { get; set; } = null!;
}
