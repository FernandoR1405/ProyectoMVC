//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Supermercado_APP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblVentaEncabezado
    {
        public tblVentaEncabezado()
        {
            this.tblventadetalles = new HashSet<tblventadetalle>();
        }
    
        public int VentEnc_Id { get; set; }
        public Nullable<int> VentEnc_NumFactura { get; set; }
        public Nullable<System.DateTime> VentEnc_Fecha { get; set; }
        public Nullable<int> Per_Id { get; set; }
        public Nullable<decimal> VentEnc_Total { get; set; }
        public Nullable<bool> VentEnc_Activo { get; set; }
        public Nullable<int> VentEnc_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> VentEnc_FechaCrea { get; set; }
        public Nullable<int> VentEnc_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> VentEnc_FechaModifica { get; set; }
    
        public virtual tblUsuario tblUsuario { get; set; }
        public virtual tblUsuario tblUsuario1 { get; set; }
        public virtual ICollection<tblventadetalle> tblventadetalles { get; set; }
        public virtual tblPersona tblPersona { get; set; }
    }
}