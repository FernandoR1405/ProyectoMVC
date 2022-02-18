using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supermercado_APP.Models
{
    [MetadataType(typeof(tblventadetalleMeData))]
    public partial class tblventadetalle
    {
    }

    public class tblventadetalleMeData
    {
        [Display(Name = "Id")]
        public int VentDet_Id { get; set; }

        [Display(Name = "Encabezado")]
        public Nullable<int> VentEnc_Id { get; set; }

        [Display(Name = "Producto")]
        public Nullable<int> Prd_Id { get; set; }

        [Display(Name = "Precio")]
        public Nullable<decimal> VentDet_Precio { get; set; }

        [Display(Name = "Cantidad")]
        public Nullable<int> VentDet_Cantidad { get; set; }

        [Display(Name = "Descuento")]
        public Nullable<decimal> VentDet_Descuento { get; set; }

        [Display(Name = "Impuesto")]
        public string VentDet_Impuesto { get; set; }

        [Display(Name = "Sub Total")]
        public Nullable<decimal> VentDet_Subtotal { get; set; }

        [Display(Name = "Estado")]
        public Nullable<bool> VentDet_Activo { get; set; }

        [Display(Name = "Creado Por")]
        public Nullable<int> VentDet_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public Nullable<System.DateTime> VentDet_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public Nullable<int> VentDet_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificacion")]
        public Nullable<System.DateTime> VentDet_FechaModifica { get; set; }
    }
}