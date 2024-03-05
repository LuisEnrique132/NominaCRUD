using System;
using System.Collections.Generic;

namespace NominaCRUD.Models;

public partial class Deduccione
{
    public int DeduccionId { get; set; }

    public string NombreDeduccion { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<DeduccionesEmpleado> DeduccionesEmpleados { get; set; } = new List<DeduccionesEmpleado>();
}
