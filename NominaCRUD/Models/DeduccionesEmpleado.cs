using System;
using System.Collections.Generic;

namespace NominaCRUD.Models;

public partial class DeduccionesEmpleado
{
    public int DeduccionEmpleadoId { get; set; }

    public int EmpleadoId { get; set; }

    public int DeduccionId { get; set; }

    public decimal Monto { get; set; }

    public DateOnly FechaDeduccion { get; set; }

    public virtual Deduccione Deduccion { get; set; } = null!;

    public virtual Empleado Empleado { get; set; } = null!;
}
