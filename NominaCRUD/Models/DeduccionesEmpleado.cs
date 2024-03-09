using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NominaCRUD.Models;

public partial class DeduccionesEmpleado
{
    public int DeduccionEmpleadoId { get; set; }

    public int EmpleadoId { get; set; }

    public int DeduccionId { get; set; }
    [Display(Name ="Monto total de las deduciones")]
    public decimal Monto { get; set; }
    [Display(Name = "Fecha de emision de las deduciones")]

    public DateTime? FechaDeduccion { get; set; }
    [Display(Name = "Deduccion")]

    public virtual Deduccione Deduccion { get; set; } = null!;
    [Display(Name = "Codigo del Empleado")]

    public virtual Empleado Empleado { get; set; } = null!;
}
