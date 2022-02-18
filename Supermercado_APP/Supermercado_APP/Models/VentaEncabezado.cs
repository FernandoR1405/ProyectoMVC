using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supermercado_APP.Models
{
    [MetadataType(typeof(tblVentaEncabezadoMeData))]
    public partial class tblVentaEncabezado
    {
    }

    public class tblVentaEncabezadoMeData
    {
        [Display(Name = "Id")]
        public int VentEnc_Id { get; set; }

        [Display(Name = "No.Factura")]
        public Nullable<int> VentEnc_NumFactura { get; set; }

        [Display(Name = "Fecha de Venta")]
        public Nullable<System.DateTime> VentEnc_Fecha { get; set; }

        [Display(Name = "Cliente")]
        public Nullable<int> Per_Id { get; set; }

        [Display(Name = "Total")]
        public Nullable<decimal> VentEnc_Total { get; set; }

        [Display(Name = "Estado")]
        public Nullable<bool> VentEnc_Activo { get; set; }

        [Display(Name = "Creado Por")]
        public Nullable<int> VentEnc_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creacion")]
        public Nullable<System.DateTime> VentEnc_FechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public Nullable<int> VentEnc_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificacion")]
        public Nullable<System.DateTime> VentEnc_FechaModifica { get; set; }
    }
}