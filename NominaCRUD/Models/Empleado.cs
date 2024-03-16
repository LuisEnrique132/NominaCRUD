using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NominaCRUD.Models;

public partial class Empleado
{
    [Display(Name = "Id")]
    public int EmpleadoId { get; set; }
    [Display(Name = "Nombre del empleado")]
    [Required(ErrorMessage = "Este Campo es requerido")]

    public string Nombre { get; set; } = null!;
    [Display(Name = "Cedula")]
    [Required(ErrorMessage ="Este Campo es requerido")]
    [MaxLength(11, ErrorMessage ="No Cumple con lo requerido")]
    [MinLength(10, ErrorMessage = "No Cumple con lo requerido")]

    public string Cedula { get; set; } = null!;
    [Display(Name = "Codigo del empleado")]
    [Required(ErrorMessage = "Este Campo es requerido")]

    public string Codigo { get; set; } = null!;
    [Display(Name = "Departamento")]
    [Required(ErrorMessage = "Este Campo es requerido")]
    public string Departamento { get; set; } = null!;
    [Display(Name = "Salario Mensual")]
    [Required(ErrorMessage = "Este Campo es requerido")]
    public decimal SalarioBase { get; set; }
    [Display(Name = "Fecha De ingreso")]
    [Required(ErrorMessage = "Este Campo es requerido")]

    public DateTime? FechaIngreso { get; set; }

    public virtual ICollection<DeduccionesEmpleado> DeduccionesEmpleados { get; set; } = new List<DeduccionesEmpleado>();

    public virtual ICollection<IngresosEmpleado> IngresosEmpleados { get; set; } = new List<IngresosEmpleado>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
