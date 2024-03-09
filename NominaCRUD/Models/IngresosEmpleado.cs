using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NominaCRUD.Models;

public partial class IngresosEmpleado
{
    public int IngresoId { get; set; }
    [Display(Name = "Empleado")]
    public int EmpleadoId { get; set; }

    [Display(Name = "Tipo De Ingreso")]
    public int TipoIngresoId { get; set; }


    public decimal Monto { get; set; }

    [Display(Name = "Fecha de Ingreso")]
    public DateOnly FechaIngreso { get; set; }

    public virtual Empleado? Empleado { get; set; } = null!;
    [Display(Name = "Ingreso")]

    public virtual TiposIngreso? TipoIngreso { get; set; } = null!;
}
