using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NominaCRUD.Models;

public partial class Deduccione
{

    public int DeduccionId { get; set; }
 Allan

    [Display(Name ="Deduccion")]
=======
    [Display(Name ="Nombre de la deduccion")]
 master
    public string NombreDeduccion { get; set; } = null!;

    [Display(Name = "Descripcion")]

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<DeduccionesEmpleado> DeduccionesEmpleados { get; set; } = new List<DeduccionesEmpleado>();
}
