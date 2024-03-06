using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NominaCRUD.Models;

public partial class Pago
{
    public int PagoId { get; set; }

    [Display(Name = "Empleados")]
    public int EmpleadoId { get; set; }

    [Display(Name = "Ingresos Totales")]
    public decimal TotalIngresos { get; set; }

    [Display(Name = "Deducciones Totales")]
    public decimal TotalDeducciones { get; set; }

    [Display(Name = "Salario Neto")]
    public decimal SalarioNeto { get; set; }

    [Display(Name = "Fecha de Pago")]
    public DateTime? FechaPago { get; set; }

    public virtual Empleado? Empleado { get; set; } = null!;
}
