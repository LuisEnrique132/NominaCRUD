using System;
using System.Collections.Generic;

namespace NominaCRUD.Models;

public partial class Pago
{
    public int PagoId { get; set; }

    public int EmpleadoId { get; set; }

    public decimal TotalIngresos { get; set; }

    public decimal TotalDeducciones { get; set; }

    public decimal SalarioNeto { get; set; }

    public DateOnly FechaPago { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;
}
