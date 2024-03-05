using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NominaCRUD.Models;

public partial class TiposIngreso
{
    public int TipoIngresoId { get; set; }
    [Display(Name ="Tipo de Ingreso")]
    public string NombreTipo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<IngresosEmpleado> IngresosEmpleados { get; set; } = new List<IngresosEmpleado>();
}
