using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NominaCRUD.Models
{
    public class AsientoContable
    {
        [Required(ErrorMessage = "La descripción es requerida.")]
        public string? Descripcion { get; set;  }

        [Required(ErrorMessage = "El auxiliar es requerido.")]
        public int Auxiliar { get; set; } = 2;

        [Required(ErrorMessage = "La fecha es requerida.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El monto es requerido.")]
        public decimal Monto { get; set; }
           public List<Transaccion> Transaccions { get; set; }  
        public  virtual Estado? estado { get; set; } 
        public virtual Moneda? moneda { get; set; }  

    }
    
    

    public class Transaccion
    {
        [Required(ErrorMessage = "La cuenta es requerida.")]
        public virtual Cuenta? Cuenta { get; set; }

        [Required(ErrorMessage = "El tipo de movimiento es requerido.")]
        public virtual TipoMovimiento? TipoMovimiento { get; set; }

        [Required(ErrorMessage = "El monto de la transacción es requerido.")]
        public decimal Monto { get; set; }
    }
    public class Estado
    {
        public int id { get; set; } 
        public string? Descripcion { get; set; }


    }
    public class Moneda
    {
        public int id { get; set; }
        public string? CoodigoIso { get; set; }
        public string? Descripcion { get; set; }
        public decimal TasaCambio { get; set; }
    }
    public  class TipoMovimiento
    {
        public int id { get; set; }
        public string? Descripcion { get; set; }

    }
    public class Cuenta
    {
        public int id { get; set; }
        public string? Descripcion { get; set; }
        public int Tipo { get; set; }
    }

            
}

