using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NominaCRUD.Models;

public partial class Pago
{
    public int PagoId { get; set; }

    [Display(Name = "Empleados")]
    [Required(ErrorMessage = "Este Campo es requerido")]
    public int EmpleadoId { get; set; }

    [Display(Name = "Ingresos Totales")]
    [Required(ErrorMessage = "Este Campo es requerido")]
    public decimal TotalIngresos { get; set; }

    [Display(Name = "Deducciones Totales")]
    [Required(ErrorMessage = "Este Campo es requerido")]
    public decimal TotalDeducciones { get; set; }

    [Display(Name = "Salario Neto")]
    [Required(ErrorMessage = "Este Campo es requerido")]
    public decimal SalarioNeto { get; set; }

 
    [Display(Name = "Fecha de Pago")]
    [Required(ErrorMessage = "Este Campo es requerido")]

    public DateTime? FechaPago { get; set; }

    public virtual Empleado? Empleado { get; set; } = null!;
}
