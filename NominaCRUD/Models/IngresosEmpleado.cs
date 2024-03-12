using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NominaCRUD.Models;

public partial class IngresosEmpleado
{
    public int IngresoId { get; set; }
    [Display(Name = "Empleado")]
    [Required(ErrorMessage = "Este Campo es requerido")]
    public int EmpleadoId { get; set; }

    [Display(Name = "Tipo De Ingreso")]
    [Required(ErrorMessage = "Este Campo es requerido")]
    public int TipoIngresoId { get; set; }
     [Required(ErrorMessage = "Este Campo es requerido")]

    public decimal Monto { get; set; }

    [Display(Name = "Fecha de Ingreso")]
    [Required(ErrorMessage = "Este Campo es requerido")]
    public DateOnly FechaIngreso { get; set; }

    public virtual Empleado? Empleado { get; set; } = null!;
    [Display(Name = "Ingreso")]
    

    public virtual TiposIngreso? TipoIngreso { get; set; } = null!;
}
