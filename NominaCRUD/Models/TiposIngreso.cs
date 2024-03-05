using System;
using System.Collections.Generic;

namespace NominaCRUD.Models;

public partial class TiposIngreso
{
    public int TipoIngresoId { get; set; }

    public string NombreTipo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<IngresosEmpleado> IngresosEmpleados { get; set; } = new List<IngresosEmpleado>();
}
