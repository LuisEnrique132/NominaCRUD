using System;
using System.Collections.Generic;

namespace NominaCRUD.Models;

public partial class IngresosEmpleado
{
    public int IngresoId { get; set; }

    public int EmpleadoId { get; set; }

    public int TipoIngresoId { get; set; }

    public decimal Monto { get; set; }

    public DateOnly FechaIngreso { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual TiposIngreso TipoIngreso { get; set; } = null!;
}
