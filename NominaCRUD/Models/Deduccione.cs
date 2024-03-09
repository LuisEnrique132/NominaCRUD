using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NominaCRUD.Models;

public partial class Deduccione
{

    public int DeduccionId { get; set; }


    [Display(Name ="Deduccion")]

 
    public string NombreDeduccion { get; set; } = null!;

    [Display(Name = "Descripcion")]

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<DeduccionesEmpleado> DeduccionesEmpleados { get; set; } = new List<DeduccionesEmpleado>();
}
