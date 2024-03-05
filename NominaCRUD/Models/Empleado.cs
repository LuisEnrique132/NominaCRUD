using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NominaCRUD.Models;

public partial class Empleado
{
    [Display(Name = "Id")]
    public int EmpleadoId { get; set; }
    [Display(Name = "Nombre del empleado")]

    public string Nombre { get; set; } = null!;
    [Display(Name = "Cedula")]

    public string Cedula { get; set; } = null!;
    [Display(Name = "Codigo del empleado")]


    public string Codigo { get; set; } = null!;
    [Display(Name = "Departamento")]

    public string Departamento { get; set; } = null!;
    [Display(Name = "Salario Mensual")]

    public decimal SalarioBase { get; set; }
    [Display(Name = "Fecha De ingreso")]

    public DateTime? FechaIngreso { get; set; }

    public virtual ICollection<DeduccionesEmpleado> DeduccionesEmpleados { get; set; } = new List<DeduccionesEmpleado>();

    public virtual ICollection<IngresosEmpleado> IngresosEmpleados { get; set; } = new List<IngresosEmpleado>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
