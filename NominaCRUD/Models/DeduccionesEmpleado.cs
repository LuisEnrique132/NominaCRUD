using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NominaCRUD.Models;

public partial class DeduccionesEmpleado
{
    public int DeduccionEmpleadoId { get; set; }
    [Display(Name = "Empleado")]
    public int EmpleadoId { get; set; }
    [Display(Name = "Deduccion")]
    public int DeduccionId { get; set; }
  
    public decimal Monto { get; set; }


    [Display(Name = "Fecha Deduccion")]
    public DateTime FechaDeduccion { get; set; }


    public virtual Deduccione? Deduccion { get; set; } = null;
    //[Display(Name = "Codigo del ")]

    public virtual Empleado? Empleado { get; set; } = null;
}
